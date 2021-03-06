using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DOTNETCoreCRUD.Data;
using DOTNETCoreCRUD.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DOTNETCoreCRUD.Controllers
{
    public class BookController : Controller
    {
        private readonly IConfiguration _configuration;
        public BookController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        // GET: Book
        public IActionResult Index()
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter sda = new SqlDataAdapter("BookViewAll", sqlConnection);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.Fill(dtbl);
            }
            return View(dtbl);
        }

        // GET: Book/AddOrEdit/
        public IActionResult AddOrEdit(int? id) 
        {
            BookViewModel bookViewModel = new BookViewModel();
            if (id > 0)
                bookViewModel = FetchBookByID(id);
            return View(bookViewModel);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(int id, [Bind("BookID,Title,Author,Price")] BookViewModel bookViewModel)
        {          
            if (ModelState.IsValid)
            {
                using(SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("BookAddOrEdit", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("BookID", bookViewModel.BookID);
                    cmd.Parameters.AddWithValue("Title", bookViewModel.Title);
                    cmd.Parameters.AddWithValue("Author", bookViewModel.Author);
                    cmd.Parameters.AddWithValue("Price", bookViewModel.Price);
                    cmd.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bookViewModel);
        }

        // GET: Book/Delete/5
        public IActionResult Delete(int? id)
        {
            BookViewModel bookViewModel = FetchBookByID(id);
            return View();
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("BookDeleteByID", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("BookID", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction(nameof(Index));
        }
        public BookViewModel FetchBookByID(int? id)
        {
            BookViewModel bookViewModel = new BookViewModel();
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                DataTable dtbl = new DataTable();
                sqlConnection.Open();
                SqlDataAdapter sda = new SqlDataAdapter("BookViewByID", sqlConnection);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("BookID", id);
                sda.Fill(dtbl);
                if(dtbl.Rows.Count == 1)
                {
                    bookViewModel.BookID = Convert.ToInt32(dtbl.Rows[0]["BookID"].ToString());
                    bookViewModel.Title = dtbl.Rows[0]["Title"].ToString();
                    bookViewModel.Author = dtbl.Rows[0]["Author"].ToString();
                    bookViewModel.Price = Convert.ToInt32(dtbl.Rows[0]["Price"].ToString());
                }
                return bookViewModel;
            }
        }
    }
}
