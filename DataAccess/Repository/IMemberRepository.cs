using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetAll();
        void Add(Member member);
        void Update(Member member);
        void Delete(Member member);

        int GetMaxMemberId();

        Member GetMemberByEmail(string email);
    }
}
