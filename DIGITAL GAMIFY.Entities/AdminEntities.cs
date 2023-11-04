using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DIGITAL_GAMIFY.Entities
{
    public class AdminLoginEntities
    {

        [Required(ErrorMessage = "Please enter UserName.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter Password.")]
        public string Password { get; set; }

    }

    public class AdminEntities
    {

        #region Private Variables

        private string firstName, lastName = string.Empty;

        #endregion

        public long AdminId { get; set; }
        public string FirstName { get { return this.firstName; } set { this.firstName = Settings.SetFont(value); } }
        public string LastName { get { return this.lastName; } set { this.lastName = Settings.SetFont(value); } }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDay { get { return Settings.SetDateTimeFormat(this.CreatedDate); } }
    }
}
