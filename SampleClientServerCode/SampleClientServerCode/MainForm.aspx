<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainForm.aspx.cs" Inherits="SampleClientServerCode.MainForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Test-Application</title>
    <script type="text/javascript">

        var timer;
        var threadID;
        function startTimer(val)
        {
            threadID = val;
            timer = setInterval(checkThreadFinished, 5000);
        }

        function checkThreadFinished() {
            PageMethods.isThreadFinished(threadID, onSuccess, onFailure);
            function onSuccess(result) {
                var lblInfo = document.getElementById("lblInfo");
                var lblThreadID = document.getElementById("lblThreadID");
                if (result == true) {
                    lblInfo.innerHTML = "Thread finished, download File...";
                    lblThreadID.innerHTML = "";
                    clearInterval(timer);
                    downloadFile();
                    btnTest.disabled = false;
                }
            }
            function onFailure(error) {
                alert(error);
            }
        }
        
        function downloadFile()
        {
            PageMethods.GetFile(onSuccess, onFailure);
            function onSuccess(result) {
                var lblInfo = document.getElementById("lblInfo");
                lblInfo.innerHTML = "File stored at: " + result;
                setInterval(function () { lblInfo.innerHTML = ""; }, 5000);
            }
            function onFailure(error) {
                alert(error);
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <asp:Button ID="btnTest" Text="start Thread" runat="server" OnClick="btnTest_Click" style="display:normal"/>
    <p>
        <asp:Label ID="lblThreadID" runat="server" Text=""></asp:Label>
    </p>
    <p>
        <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
    </p>
    </form>
</body>
</html>
