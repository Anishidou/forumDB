﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace forumDB.Model
{
    public partial class Usuario
    {

        [NotMapped]
        public String NomeCurso { get; set; }
        [NotMapped]
        public IFormFile FotoRaw { get; set; }
    }
}