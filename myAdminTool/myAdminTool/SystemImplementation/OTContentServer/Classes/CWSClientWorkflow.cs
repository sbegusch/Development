using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using myAdminTool.OTCSWorkflowService;

namespace myAdminTool.OTCS
{
    public partial class CWSClient
    {
        private const int DEFAULT_PAGE_SIZE = 100;

        internal ProcessResult ListProcesses()
        {
            return ListProcesses(DEFAULT_PAGE_SIZE);
        }

        internal ProcessResult ListProcesses( int pageSize )
        {
            PageHandle pageHandle;
            ProcessSearchOptions options = new ProcessSearchOptions();

            options.PageSize = pageSize;
            options.RetrieveActivities = true;

            ReAuthenticateIfRequired();
            pageHandle = fWorkflowService.ListProcesses(ref fWorkflowAuthentication, options);
            UpdateAuthenticationTokens(fWorkflowAuthentication.AuthenticationToken);

            return ListProcesses(pageHandle);
        }

        internal ProcessResult ListProcesses(PageHandle pageHandle)
        {
            ReAuthenticateIfRequired();
            ProcessResult processResult = fWorkflowService.GetListProcessesResults(ref fWorkflowAuthentication, pageHandle);
            UpdateAuthenticationTokens(fWorkflowAuthentication.AuthenticationToken);

            return processResult;
        }

        internal WorkItemResult ListWorkItems()
        {
            return ListWorkItems(DEFAULT_PAGE_SIZE);
        }

        internal WorkItemResult ListWorkItems(int pageSize)
        {
            PageHandle pageHandle = new PageHandle();
            pageHandle.PageSize = pageSize;

            return ListWorkItems(pageHandle);
        }

        internal WorkItemResult ListWorkItems(PageHandle pageHandle)
        {
            ReAuthenticateIfRequired();
            WorkItemResult result = fWorkflowService.ListWorkItems(ref fWorkflowAuthentication, pageHandle);
            UpdateAuthenticationTokens(fWorkflowAuthentication.AuthenticationToken);

            return result;
        }

        internal ApplicationData[] GetWorkItemData(ProcessInstance selectedPI, int ActivityID)
        {
            ReAuthenticateIfRequired();
            //ApplicationData[] appdata = fWorkflowService.GetWorkItemData(ref fWorkflowAuthentication, selectedPI.ProcessID, selectedPI.SubProcessID);
            ApplicationData[] appData = fWorkflowService.GetWorkItemData(ref fWorkflowAuthentication, selectedPI.ProcessID, selectedPI.SubProcessID, ActivityID);
            return appData;
        }

        internal void UpdateWorkItem(ProcessInstance selectedPI, int ActivityID, ApplicationData[] appData)
        {
            ReAuthenticateIfRequired();
            //ApplicationData[] appdata = fWorkflowService.GetWorkItemData(ref fWorkflowAuthentication, selectedPI.ProcessID, selectedPI.SubProcessID);
            fWorkflowService.UpdateWorkItemData(ref fWorkflowAuthentication, selectedPI.ProcessID, selectedPI.SubProcessID, ActivityID, appData);
        }
    }
}
