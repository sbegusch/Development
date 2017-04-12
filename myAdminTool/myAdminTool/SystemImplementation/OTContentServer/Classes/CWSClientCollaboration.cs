using System;
using System.IO;

using myAdminTool.OTCSCollaboration;

namespace myAdminTool.OTCS
{
    public partial class CWSClient
    {
        internal TopicInfo PostTopic(
                int discussionID,
                string subject,
                string comments,
                int? attachmentID,
                string attachmentPath)
        {
            Attachment attachment = MakeAttachment(attachmentPath);

            ReAuthenticateIfRequired();
            TopicInfo topicInfo = fCollaborationService.PostTopic(
                    ref fCollaborationAuthentication,
                    discussionID,
                    subject,
                    comments,
                    attachmentID,
                    attachment);
            UpdateAuthenticationTokens(fCollaborationAuthentication.AuthenticationToken);

            return topicInfo;
        }

        internal ReplyInfo PostReply(
                int topicReplyID,
                string subject,
                string comments,
                int? attachmentID,
                string attachmentPath)
        {
            Attachment attachment = MakeAttachment(attachmentPath);

            ReAuthenticateIfRequired();
            ReplyInfo replyInfo = fCollaborationService.PostReply(
                    ref fCollaborationAuthentication,
                    topicReplyID,
                    subject,
                    comments,
                    attachmentID,
                    attachment);
            UpdateAuthenticationTokens(fCollaborationAuthentication.AuthenticationToken);

            return replyInfo;
        }

        internal Node CreateDiscussion(int parentID, string name, int createdBy)
        {
            DiscussionInfo info = new DiscussionInfo();
            info.Name = name;
            info.CreatedBy = createdBy;
            info.ParentID = parentID;

            ReAuthenticateIfRequired();
            Node node = fCollaborationService.CreateDiscussion(ref fCollaborationAuthentication, info);
            UpdateAuthenticationTokens(fCollaborationAuthentication.AuthenticationToken);

            return node;
        }

        internal Node CreateMilestone(int parentID, string name)
        {
            MilestoneInfo info = new MilestoneInfo();
            info.Name = name;
            info.ParentID = parentID;

            // create the node
            ReAuthenticateIfRequired();
            Node node = fCollaborationService.CreateMilestone(ref fCollaborationAuthentication, info);
            UpdateAuthenticationTokens(fCollaborationAuthentication.AuthenticationToken);

            return node;
        }

        internal Node CreateProject(int parentID, string name)
        {
            ProjectInfo info = new ProjectInfo();
            info.ParentID = parentID;
            info.Name = name;

            ReAuthenticateIfRequired();
            Node node = fCollaborationService.CreateProject(ref fCollaborationAuthentication, info);
            UpdateAuthenticationTokens(fCollaborationAuthentication.AuthenticationToken);

            return node;
        }

        internal Node CreateTask(
                int parentID,
                string name,
                int assignedTo,
                string comments,
                string instructions,
                DateTime? startDate,
                DateTime? dueDate,
                TaskPriority priority,
                TaskStatus status)
        {
            TaskInfo info = new TaskInfo();

            info.Name = name;
            info.ParentID = parentID;
            info.AssignedTo = assignedTo;
            info.Comments = comments;
            info.Instructions = instructions;
            info.StartDate = startDate;
            info.DueDate = dueDate;
            info.Priority = priority;
            info.Status = status;

            ReAuthenticateIfRequired();
            Node node = fCollaborationService.CreateTask(ref fCollaborationAuthentication, info);
            UpdateAuthenticationTokens(fCollaborationAuthentication.AuthenticationToken);

            return node;
        }

        internal Node CreateTaskGroup(int parentID, string name)
        {
            TaskGroupInfo info = new TaskGroupInfo();

            info.Name = name;
            info.ParentID = parentID;

            ReAuthenticateIfRequired();
            Node node = fCollaborationService.CreateTaskGroup(ref fCollaborationAuthentication, info);
            UpdateAuthenticationTokens(fCollaborationAuthentication.AuthenticationToken);

            return node;
        }

        internal Node CreateTaskList(int parentID, string name)
        {
            TaskListInfo info = new TaskListInfo();

            info.Name = name;
            info.ParentID = parentID;

            ReAuthenticateIfRequired();
            Node node = fCollaborationService.CreateTaskList(ref fCollaborationAuthentication, info);
            UpdateAuthenticationTokens(fCollaborationAuthentication.AuthenticationToken);

            return node;
        }


        /// <summary>
        /// Get an Attachment object from a local file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private Attachment MakeAttachment(string path)
        {
            Attachment attachment = null;


            if (null != path && path.Length > 0)
            {
                attachment = new OTCSCollaboration.Attachment();

                FileInfo fileInfo = new System.IO.FileInfo(path);
                byte[] contents = File.ReadAllBytes(path);

                attachment.Contents = contents;
                attachment.CreatedDate = fileInfo.CreationTime;
                attachment.FileName = fileInfo.Name;
                attachment.FileSize = contents.Length;
                attachment.ModifiedDate = fileInfo.LastWriteTime;
            }

            return attachment;
        }
    }
}
