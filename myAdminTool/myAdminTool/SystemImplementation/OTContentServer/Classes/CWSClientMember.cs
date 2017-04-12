using System;

using myAdminTool.OTCSMemberService;

namespace myAdminTool.OTCS
{
    public partial class CWSClient
    {
        internal string GetMemberDisplayName(int memberID)
        {
            Member member;

            string name = "";

            member = GetMemberByID(memberID);

            if (member != null)
            {
                name = member.DisplayName;
            }

            return name;
        }

        internal Member GetMemberByID(int memberID)
        {
            ReAuthenticateIfRequired();
            Member member = fMemberService.GetMemberById(ref fMemberAuthentication, memberID);
            UpdateAuthenticationTokens(fMemberAuthentication.AuthenticationToken);

            return member;
        }

        internal Member GetMemberByLoginName(string memberName)
        {
            ReAuthenticateIfRequired();
            Member user = fMemberService.GetMemberByLoginName(ref fMemberAuthentication, memberName);
            UpdateAuthenticationTokens(fMemberAuthentication.AuthenticationToken);
            
            return user;
        }

        internal User GetAuthenticatedUser()
        {
            ReAuthenticateIfRequired();
            User user = fMemberService.GetAuthenticatedUser(ref fMemberAuthentication);
            UpdateAuthenticationTokens(fMemberAuthentication.AuthenticationToken);

            return user;
        }

        internal Member CreateMember(Member member)
        {
            ReAuthenticateIfRequired();
            Member newMember = fMemberService.CreateMember(ref fMemberAuthentication, member);
            UpdateAuthenticationTokens(fMemberAuthentication.AuthenticationToken);

            return newMember;
        }

        internal Member UpdateMember(Member member)
        {
            ReAuthenticateIfRequired();
            Member newMember = fMemberService.UpdateMember(ref fMemberAuthentication, member);
            UpdateAuthenticationTokens(fMemberAuthentication.AuthenticationToken);

            return newMember;
        }

        internal void DeleteMember(int memberID)
        {
            ReAuthenticateIfRequired();
            fMemberService.DeleteMember(ref fMemberAuthentication, memberID);
            UpdateAuthenticationTokens(fMemberAuthentication.AuthenticationToken);
        }

        internal MemberSearchResults SearchForGroupsByName(string groupName)
        {
            MemberSearchOptions searchOptions = new MemberSearchOptions();

            // Set the search options
            searchOptions.Scope = SearchScope.SYSTEM;
            searchOptions.Filter = SearchFilter.GROUP;
            searchOptions.Column = SearchColumn.NAME;
            searchOptions.Search = groupName;

            return SearchForMembers(searchOptions);
        }

        internal MemberSearchResults SearchForMembers(MemberSearchOptions searchOptions)
        {
            // Store search criteria
            ReAuthenticateIfRequired();
            PageHandle pageHandle = fMemberService.SearchForMembers(ref fMemberAuthentication, searchOptions);
            UpdateAuthenticationTokens(fMemberAuthentication.AuthenticationToken);

            return SearchForMembers(pageHandle);
        }

        internal MemberSearchResults SearchForMembers(PageHandle pageHandle)
        {
            ReAuthenticateIfRequired();
            MemberSearchResults results = fMemberService.GetSearchResults(ref fMemberAuthentication, pageHandle);
            UpdateAuthenticationTokens(fMemberAuthentication.AuthenticationToken);

            return results;
        }
    }
}
