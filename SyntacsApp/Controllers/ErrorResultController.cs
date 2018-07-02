﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SyntacsApp.Data;
using SyntacsApp.Models;

namespace SyntacsApp.Controllers
{
    public class ErrorResultController : Controller
    {
        private readonly SyntacsDbContext _context;
        /// <summary>
        /// Constructor to set up our dependency injection
        /// </summary>
        /// <param name="context">Our DbContext</param>
        public ErrorResultController(SyntacsDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (id.HasValue)
            {
                return View(await ErrorResultViewModel.ViewDetails(id.Value, _context));
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> Create(int id, [Bind("ID,Alias,CommentBody")]Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.ErrExampleID = id;
                await _context.Comments.AddAsync(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { id });
            }

            return RedirectToAction("Index", new { id });
        }
  
    }
}
