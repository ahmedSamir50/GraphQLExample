using HotChocolate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLOne.Models
{
    // API Graph QL Documentation
    // [GraphQLDescription("Represents any Software or services that has a command line interface . \"CLI\"")]  moved to the types class
    public class Platform
    {
        [Key]
        public int Id { get; set; }
        [Required]
        // [GraphQLDescription("Represents Platform Name ")] moved to the types class
        public string Name { get; set; }
        //[GraphQLDescription("Represents Platform Under License of Name ")] moved to the types class
        public string LicenseKey { get; set; }
       // [GraphQLDescription("Represents Platform Related Known Commands  ")] moved to the types class

        public virtual ICollection<Command> Commands {get;set;} = new List<Command>();
    }
}
