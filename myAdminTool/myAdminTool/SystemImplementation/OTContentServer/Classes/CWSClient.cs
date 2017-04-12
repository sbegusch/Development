using System;
using System.ServiceModel;

using myAdminTool.OTCSAuthentication;
using myAdminTool.OTCSContentService;
using myAdminTool.OTCSDocumentManagement;
using myAdminTool.OTCSMemberService;
using myAdminTool.OTCSWorkflowService;

namespace myAdminTool.OTCS
{
    public partial class CWSClient
    {
        // services
        private OTCSAuthentication.AuthenticationClient fAuthService;
        //private RCSAuthentication.AuthenticationClient fRCSAuthService;
        private OTCSContentService.ContentServiceClient fContentService;
        private OTCSCollaboration.CollaborationClient fCollaborationService;
        private OTCSDocumentManagement.DocumentManagementClient fDocManService;
        private OTCSMemberService.MemberServiceClient fMemberService;
        private OTCSWorkflowService.WorkflowServiceClient fWorkflowService;

        // authentication credentials
        private OTCSContentService.OTAuthentication fContentAuthentication;
        private OTCSDocumentManagement.OTAuthentication fDocsAuthentication;
        private OTCSMemberService.OTAuthentication fMemberAuthentication;
        private OTCSWorkflowService.OTAuthentication fWorkflowAuthentication;
        private OTCSCollaboration.OTAuthentication fCollaborationAuthentication;

        private bool fSSLEnabled = false;
        private DateTime? fAuthExpiration = null;

        private string fUsername;
        private string fPassword;
        private string fServiceRoot;
        private string fServiceSuffix;
        private string fRCSAuthServiceURL;
        private AuthenticationMode fAuthMode;
        
        internal CWSClient( 
                string username, 
                string password,
                string serviceRoot,
                string serviceSuffix, 
                string rcsAuthServiceURL,
                AuthenticationMode authMode )
        {
            fUsername = username;
            fPassword = password;
            fServiceRoot = serviceRoot;
            fServiceSuffix = serviceSuffix;
            fRCSAuthServiceURL = rcsAuthServiceURL;
            fAuthMode = authMode;

            Authenticate();
        }

        private void Authenticate()
        {
            string token;

            if (IsSSLURL(fServiceRoot))
            {
                fSSLEnabled = true;
            }

            if (fAuthMode == AuthenticationMode.CWSAuthentication || fAuthMode == AuthenticationMode.SingleSignOn)
            {
                token = AuthenticateUser(fServiceRoot, fServiceSuffix, fUsername, fPassword);
            }
            else // AuthenticationMode.RCSAuthentication || AuthenticationMode.LegacyRCSAuthentication
            {
                token = AuthenticateUserRCS(fServiceRoot, fServiceSuffix, fRCSAuthServiceURL, fUsername, fPassword);
            }

            InitServices(fServiceRoot, fServiceSuffix, token);

        }

        private string AuthenticateUser(
            string serviceRoot,
            string serviceSuffix, 
            string username, 
            string password)
        {
            string token = "";

            InitAuthenticationService(serviceRoot, serviceSuffix);

            if (fAuthMode == AuthenticationMode.SingleSignOn)
            {
                BasicHttpBinding binding = (BasicHttpBinding)fAuthService.Endpoint.Binding;
                binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            }
            else
            {
                token = fAuthService.AuthenticateUser(username, password);

                // get the expiry date for the token
                OTCSAuthentication.OTAuthentication otAuth = new OTCSAuthentication.OTAuthentication();
                otAuth.AuthenticationToken = token;

                fAuthExpiration = fAuthService.GetSessionExpirationDate(ref otAuth);

                token = otAuth.AuthenticationToken;
            }

            return token;
        }

        private string AuthenticateUserRCS(
            string cwsServiceRoot,
            string cwsServiceSuffix,
            string rcsAuthServiceURL,
            string username,
            string password)
        {
            string token = "";

            InitRCSAuthenticationService(rcsAuthServiceURL);

            //RCSAuthentication.OTAuthentication otAuthRCS = new RCSAuthentication.OTAuthentication();

            //token = fRCSAuthService.AuthenticateApplication(ref otAuthRCS, username, password);

            if ( fAuthMode.Equals( AuthenticationMode.LegacyRCSAuthentication ) )
            {
                InitAuthenticationService(cwsServiceRoot, cwsServiceSuffix);

                // get the expiry date for the token
                OTCSAuthentication.OTAuthentication otAuthCS = new OTCSAuthentication.OTAuthentication();
                otAuthCS.AuthenticationToken = token;

                fAuthExpiration = fAuthService.GetSessionExpirationDate(ref otAuthCS);

                token = otAuthCS.AuthenticationToken;
            }

            return token;
        }

        private bool IsSSLURL(string url)
        {
            return url.StartsWith("https", StringComparison.CurrentCultureIgnoreCase);
        }

        private void InitAuthenticationService(string serviceRoot, string serviceSuffix)
        {
            fAuthService = new AuthenticationClient();
            fAuthService.Endpoint.Address = new EndpointAddress(serviceRoot + "Authentication" + serviceSuffix);

            if (fSSLEnabled)
            {
                fAuthService.Endpoint.Binding = new BasicHttpBinding("BasicHttpBinding_Authentication_SSL");
            }
        }

        private void InitRCSAuthenticationService(string rcsAuthServiceURL)
        {
            //fRCSAuthService = new RCSAuthentication.AuthenticationClient();
            //fRCSAuthService.Endpoint.Address = new EndpointAddress(rcsAuthServiceURL);

            if (IsSSLURL(rcsAuthServiceURL))
            {
                //fRCSAuthService.Endpoint.Binding = new BasicHttpBinding("AuthenticationPortBinding_SSL");
            }
        }

        private void InitServices(string serviceRoot, string serviceSuffix, string token)
        {
            fDocManService = new DocumentManagementClient();
            fMemberService = new MemberServiceClient();
            fWorkflowService = new WorkflowServiceClient();
            fCollaborationService = new OTCSCollaboration.CollaborationClient();
            fContentService = new OTCSContentService.ContentServiceClient();

            fDocManService.Endpoint.Address = new EndpointAddress(serviceRoot + "DocumentManagement" + serviceSuffix);
            fMemberService.Endpoint.Address = new EndpointAddress(serviceRoot + "MemberService" + serviceSuffix);
            fWorkflowService.Endpoint.Address = new EndpointAddress(serviceRoot + "WorkflowService" + serviceSuffix);
            fCollaborationService.Endpoint.Address = new EndpointAddress(serviceRoot + "Collaboration" + serviceSuffix);
            fContentService.Endpoint.Address = new EndpointAddress(serviceRoot + "ContentService" + serviceSuffix);

            if (fSSLEnabled)
            {
                fDocManService.Endpoint.Binding = new BasicHttpBinding("BasicHttpBinding_DocumentManagement_SSL");
                fMemberService.Endpoint.Binding = new BasicHttpBinding("BasicHttpBinding_MemberService_SSL");
                fWorkflowService.Endpoint.Binding = new BasicHttpBinding("BasicHttpBinding_WorkflowService_SSL");
                fCollaborationService.Endpoint.Binding = new BasicHttpBinding("BasicHttpBinding_Collaboration_SSL");
            }

            // Set up the authentication token in the SOAP Header for the doc and user services
            fDocsAuthentication = new OTCSDocumentManagement.OTAuthentication();
            fDocsAuthentication.AuthenticationToken = token;

            fMemberAuthentication = new OTCSMemberService.OTAuthentication();
            fMemberAuthentication.AuthenticationToken = token;

            fWorkflowAuthentication = new OTCSWorkflowService.OTAuthentication();
            fWorkflowAuthentication.AuthenticationToken = token;

            fCollaborationAuthentication = new OTCSCollaboration.OTAuthentication();
            fCollaborationAuthentication.AuthenticationToken = token;

            fContentAuthentication = new OTCSContentService.OTAuthentication();
            fContentAuthentication.AuthenticationToken = token;

            if (fAuthMode == AuthenticationMode.SingleSignOn)
            {
                BasicHttpBinding binding;

                binding = (BasicHttpBinding)fDocManService.Endpoint.Binding;
                binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;

                binding = (BasicHttpBinding)fMemberService.Endpoint.Binding;
                binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;

                binding = (BasicHttpBinding)fWorkflowService.Endpoint.Binding;
                binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;

                binding = (BasicHttpBinding)fContentService.Endpoint.Binding;
                binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;

                binding = (BasicHttpBinding)fCollaborationService.Endpoint.Binding;
                binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            }
        }

        private void ReAuthenticateIfRequired()
        {
            // fAuthExpiration will only be set for CWSAuthentication or LegacyRCSAuthentication
            if (fAuthExpiration != null && fAuthExpiration.Value < DateTime.Now.AddMinutes(1))
            {
                string token = "";
                
                if ( fAuthMode.Equals( AuthenticationMode.CWSAuthentication ) )
                {
                    token = fAuthService.AuthenticateUser(fUsername, fPassword);
                }
                else // AuthenticationMode.LegacyRCSAuthentication
                {
                    //RCSAuthentication.OTAuthentication otAuthRCS = new RCSAuthentication.OTAuthentication();
                    //token = fRCSAuthService.AuthenticateApplication( ref otAuthRCS, fUsername, fPassword );
                }

                OTCSAuthentication.OTAuthentication otAuth = new OTCSAuthentication.OTAuthentication();
                otAuth.AuthenticationToken = token;

                fAuthExpiration = fAuthService.GetSessionExpirationDate(ref otAuth);

                UpdateAuthenticationTokens(otAuth.AuthenticationToken);
            }
        }

        private void UpdateAuthenticationTokens(string token)
        {
            fContentAuthentication.AuthenticationToken = token;
            fDocsAuthentication.AuthenticationToken = token;
            fMemberAuthentication.AuthenticationToken = token;
            fWorkflowAuthentication.AuthenticationToken = token;
            fCollaborationAuthentication.AuthenticationToken = token;
        }
    }
}
