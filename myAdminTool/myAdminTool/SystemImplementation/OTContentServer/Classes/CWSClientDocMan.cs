using System;
using System.IO;

using myAdminTool.OTCSDocumentManagement;
using myAdminTool.OTCSContentService;
using System.Collections.Generic;

namespace myAdminTool.OTCS
{
    public partial class CWSClient
    {
        internal Node GetRootNode(string rootType)
        {
            ReAuthenticateIfRequired();
            Node node = fDocManService.GetRootNode(ref fDocsAuthentication, rootType);
            UpdateAuthenticationTokens(fDocsAuthentication.AuthenticationToken);

            return node;
        }

        internal Node GetNode(int nodeID)
        {
            ReAuthenticateIfRequired();
            Node node = fDocManService.GetNode(ref fDocsAuthentication, nodeID);
            UpdateAuthenticationTokens(fDocsAuthentication.AuthenticationToken);
            
            return node;
        }

        internal Node GetNodeTemplate(int parentID, string nodeType)
        {
            ReAuthenticateIfRequired();
            Node nodeTemplate = fDocManService.GetNodeTemplate(ref fDocsAuthentication, parentID, nodeType);
            UpdateAuthenticationTokens(fDocsAuthentication.AuthenticationToken);

            return nodeTemplate;
        }

        internal Node CreateNode(Node node)
        {
            ReAuthenticateIfRequired();
            Node newNode = fDocManService.CreateNode(ref fDocsAuthentication, node);
            UpdateAuthenticationTokens(fDocsAuthentication.AuthenticationToken);

            return newNode;
        }

        internal void UpdateNode(Node node)
        {
            ReAuthenticateIfRequired();
            fDocManService.UpdateNode(ref fDocsAuthentication, node);
            UpdateAuthenticationTokens(fDocsAuthentication.AuthenticationToken);
        }

        internal void DeleteNode(int nodeID)
        {
            ReAuthenticateIfRequired();
            fDocManService.DeleteNode(ref fDocsAuthentication, nodeID);
            UpdateAuthenticationTokens(fDocsAuthentication.AuthenticationToken);
        }

        internal void DownloadVersionContents(int nodeID, int versionNum, string fileName)
        {
            ReAuthenticateIfRequired();
            // Fetch the version contents as a stream
            String contextID = fDocManService.GetVersionContentsContext(ref fDocsAuthentication, nodeID, versionNum);
            UpdateAuthenticationTokens(fDocsAuthentication.AuthenticationToken);

            Stream stream = fContentService.DownloadContent(ref fContentAuthentication, contextID);
            UpdateAuthenticationTokens(fContentAuthentication.AuthenticationToken);

            // Write the stream to the file on disk.
            FileStream fileStream = File.OpenWrite(fileName);

            byte[] buf = new byte[1024];
            int numBytes = 0;

            while ((numBytes = stream.Read(buf, 0, buf.Length)) > 0)
            {
                fileStream.Write(buf, 0, numBytes);
            }
            fileStream.Close();
            stream.Close();
        }

        internal void AddVersion(int nodeID, FileInfo fileInfo)
        {
            FileAtts fileAtts = CreateFileAttsFromFileInfo(fileInfo);

            Stream stream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);

            ReAuthenticateIfRequired();
            String contextID = fDocManService.AddVersionContext(ref fDocsAuthentication, nodeID, null);
            UpdateAuthenticationTokens(fDocsAuthentication.AuthenticationToken);

            String objectID = fContentService.UploadContent(ref fContentAuthentication, contextID, fileAtts, stream);
            UpdateAuthenticationTokens(fContentAuthentication.AuthenticationToken);
        }

        internal Node CreateNodeAndVersion(Node node, FileInfo fileInfo)
        {
            FileAtts fileAtts = CreateFileAttsFromFileInfo(fileInfo);

            Stream stream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);

            // Create the node and add the version info at the same time
            ReAuthenticateIfRequired();
            String contextID = fDocManService.CreateNodeAndVersionContext(ref fDocsAuthentication, node);
            UpdateAuthenticationTokens(fDocsAuthentication.AuthenticationToken);

            String newNodeID = fContentService.UploadContent(ref fContentAuthentication, contextID, fileAtts, stream);
            UpdateAuthenticationTokens(fContentAuthentication.AuthenticationToken);

            // Reauthenticate in case the upload took a long time...
            ReAuthenticateIfRequired();
            Node newNode = fDocManService.GetNode(ref fDocsAuthentication, Int32.Parse(newNodeID));
            UpdateAuthenticationTokens(fDocsAuthentication.AuthenticationToken);

            return newNode;
        }

        internal Node[] ListNodes(int parentID)
        {
            return ListNodes(parentID, false);
        }

        internal Node[] ListNodes(int parentID, bool partialData)
        {
            ReAuthenticateIfRequired();
            Node[] children = fDocManService.ListNodes(ref fDocsAuthentication, parentID, partialData);
            UpdateAuthenticationTokens(fDocsAuthentication.AuthenticationToken);
            return children;
        }

        private FileAtts CreateFileAttsFromFileInfo(FileInfo fileInfo)
        {
            FileAtts fileAtts = new FileAtts();
            fileAtts.CreatedDate = fileInfo.CreationTime;
            fileAtts.FileName = fileInfo.Name;
            fileAtts.FileSize = fileInfo.Length;
            fileAtts.ModifiedDate = fileInfo.LastWriteTime;

            return fileAtts;
        }

        internal void AddCategory(Node selNode, Node catNode)
        {
            OTCSDocumentManagement.AttributeGroup categoriesChosen = fDocManService.GetCategoryTemplate(ref fDocsAuthentication, catNode.ID);
            List<OTCSDocumentManagement.AttributeGroup> tmp = new List<OTCSDocumentManagement.AttributeGroup>();

            if (selNode.Metadata.AttributeGroups != null)
            {
                for (int i = 0; i < selNode.Metadata.AttributeGroups.Length; i++)
                {
                    tmp.Add(selNode.Metadata.AttributeGroups[i]);
                }
            }
            
            tmp.Add(categoriesChosen);

            OTCSDocumentManagement.Metadata metadata = new OTCSDocumentManagement.Metadata();
            metadata.AttributeGroups = tmp.ToArray();
            selNode.Metadata = metadata;
        }

        internal void DeleteCategory(Node selNode, string catName)
        {
            List<OTCSDocumentManagement.AttributeGroup> tmp = new List<OTCSDocumentManagement.AttributeGroup>();

            for (int i = 0; i < selNode.Metadata.AttributeGroups.Length; i++)
            {
                if (selNode.Metadata.AttributeGroups[i].DisplayName != catName)
                {
                    tmp.Add(selNode.Metadata.AttributeGroups[i]);
                }

            }

            OTCSDocumentManagement.Metadata metadata = new OTCSDocumentManagement.Metadata();
            metadata.AttributeGroups = tmp.ToArray();
            selNode.Metadata = metadata;
        }
    }
}
