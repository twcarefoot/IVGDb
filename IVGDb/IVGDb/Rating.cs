//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IVGDb
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rating
    {
        public int UserID { get; set; }
        public int GameID { get; set; }
        public int Rating1 { get; set; }
    
        public virtual VideoGame VideoGame { get; set; }
        public virtual User User { get; set; }
    }
}
