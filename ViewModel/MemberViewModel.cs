using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_layout_core.Models.ViewModel
{
    public class MemberViewModel
    {
        private Member iv_member = null;
        public Member member { get { return iv_member; } }

        public MemberViewModel(Member m)
        {
            iv_member = m;
        }
        public MemberViewModel()
        {
            iv_member = new Member();
        }
        public int FUserId
        {
            get { return iv_member.FUserId; }
            set { iv_member.FUserId = value; }
        }
        public string FEmail
        {
            get { return iv_member.FEmail; }
            set { iv_member.FEmail = value; }
        }
        public string FPassword
        {
            get { return iv_member.FPassword; }
            set { iv_member.FPassword = value; }
        }
        public string FUserName
        {
            get { return iv_member.FUserName; }
            set { iv_member.FUserName = value; }
        }
        public string FGender
        {
            get { return iv_member.FGender; }
            set { iv_member.FGender = value; }
        }
        public string FUserBirthday
        {
            get { return iv_member.FUserBirthday; }
            set { iv_member.FUserBirthday = value; }
        }
        public string FUserPhone
        {
            get { return iv_member.FUserPhone; }
            set { iv_member.FUserPhone = value; }
        }        
        public string FCity
        {
            get { return iv_member.FCity; }
            set { iv_member.FCity = value; }
        }
        public string FDistrict
        {
            get { return iv_member.FDistrict; }
            set { iv_member.FDistrict = value; }
        }
        public string FAddress
        {
            get { return iv_member.FAddress; }
            set { iv_member.FAddress = value; }
        }
        public string FJoinTime
        {
            get { return iv_member.FJoinTime; }
            set { iv_member.FJoinTime = value; }
        }
        public int? Fstatus
        {
            get { return iv_member.Fstatus; }
            set { iv_member.Fstatus = value; }
        }
        public string Fsalt
        {
            get { return iv_member.Fsalt; }
            set { iv_member.Fsalt = value; }
        }  
        public string FUserPhoto
        {
            get { return iv_member.FUserPhoto; }
            set { iv_member.FUserPhoto = value; }
        }
        public IFormFile image { get; set; }
    }
}
