using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        void IMemberRepository.Add(Member member) => MemberDAO.Instance.Insert(member);

        void IMemberRepository.Delete(Member member) => MemberDAO.Instance.Delete(member);

        IEnumerable<Member> IMemberRepository.GetAll() => MemberDAO.Instance.GetAllMember();

        void IMemberRepository.Update(Member member) => MemberDAO.Instance.Update(member);

        int IMemberRepository.GetMaxMemberId() => MemberDAO.Instance.GetMaxMemberId();

        Member IMemberRepository.GetMemberByEmail(string email) => MemberDAO.Instance.GetmemberByEmail(email);
    }
}
