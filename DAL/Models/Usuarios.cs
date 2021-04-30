using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Usuarios
    {
        [Key]
        public int idUser { get; set; }
        public string UserName { get; set; }
        public string email { get; set; }
        public string pass { get; set; }
        public int roleId { get; set; }

        [ForeignKey("idUser")]
        public ICollection<UserInfo> UserInfos { get; set; }
    }
}
