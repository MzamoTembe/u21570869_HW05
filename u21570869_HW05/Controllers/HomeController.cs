using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using u21570869_HW05.Models;

namespace u21570869_HW05.Controllers
{
    public class HomeController : Controller
    {
        DataService DataService = new DataService();


        //Returns List of Books using VM for Author and Type information as awee
        [HttpGet]
        public ActionResult Index()
        {
                BooksVM model = new BooksVM
                {
                    AuthorNames = DataService.getAuthorNames(),
                    TypeNames = DataService.getTypeNames(),
                    Books = DataService.getBooks()
                };
                return View(model);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            // Get form information and books
            List<Book> books = DataService.getBooks();
            string bookname = Request["bookname"];
            string type = Request["type"];
            string author = Request["author"];
            //If form is empty
            if ((string.IsNullOrWhiteSpace(Request.Form["bookname"]) == true) && (string.IsNullOrWhiteSpace(Request.Form["type"]) == true) && (string.IsNullOrWhiteSpace(Request.Form["author"]) == true))
            {
                BooksVM model = new BooksVM
                {
                    AuthorNames = DataService.getAuthorNames(),
                    TypeNames = DataService.getTypeNames(),
                    Books = DataService.getBooks()
                };
                return View(model);
            }
            //If type is selected
            else if (string.IsNullOrWhiteSpace(Request.Form["type"]) == false)
            {
                //Return Type
                List<Book> filtered = books.Where(x => x.BookType.Name == type).ToList();
                //If author is selected, the list above will be filtered to include authors in filter
                if (string.IsNullOrWhiteSpace(Request.Form["author"]) == false)
                {
                    List<Book> fauthor = filtered.Where(x => x.Author.Name == author).ToList();
                    filtered.Clear();
                    filtered = fauthor;
                }
                //If bookname is typed, the list above will be filtered to include the booknames
                if (string.IsNullOrWhiteSpace(Request.Form["bookname"]) == false)
                {
                    List<Book> fname = filtered.Where(x => x.Name == bookname).ToList();
                    filtered.Clear();
                    filtered = fname;
                }
                //Send view model to view
                BooksVM model = new BooksVM
                {
                    AuthorNames = DataService.getAuthorNames(),
                    TypeNames = DataService.getTypeNames(),
                    Books = filtered
                };
                return View(model);
            }
            //If author is selected 
            else if (string.IsNullOrWhiteSpace(Request.Form["author"]) == false)
            {
                List<Book> filtered = books.Where(x => x.Author.Name == author).ToList();
                BooksVM model = new BooksVM
                {
                    AuthorNames = DataService.getAuthorNames(),
                    TypeNames = DataService.getTypeNames(),
                    Books = filtered
                };
                return View(model);
            }
            //If bookname is selected 
            else if (string.IsNullOrWhiteSpace(Request.Form["bookname"]) == false)
            {
                List<Book> filtered = books.Where(x => x.Name == bookname).ToList();
                BooksVM model = new BooksVM
                {
                    AuthorNames = DataService.getAuthorNames(),
                    TypeNames = DataService.getTypeNames(),
                    Books = filtered
                };
                return View(model);
            }
            // This is sort of unnecessary 
            else
            {
                BooksVM model = new BooksVM
                {
                    AuthorNames = DataService.getAuthorNames(),
                    TypeNames = DataService.getTypeNames(),
                    Books = DataService.getBooks()
                };
                return View(model);
            }
        }
        
        // Get borrows for specific book and book details (ID obtained from Index View - View button)
        [HttpGet]
        public ActionResult BookDetails(int ID)
        {
            BookDetailsVM bookdetails = new BookDetailsVM
            {
                Borrows = DataService.getBorrowedBook(ID),
                myBook = DataService.getBookbyID(ID)
            };
            return View(bookdetails);
        }

        // List of students
        public ActionResult Students(int ID)
        {
            //Form information and student list
            string studentname = Request["studentname"];
            string studentclass = Request["studentclass"];
            List<Student> students = DataService.getStudents();
            List<Student> filtered = new List<Student>();
            // If both are empty, just retur empty view
            if ((string.IsNullOrWhiteSpace(Request.Form["studentname"]) == true) && (string.IsNullOrWhiteSpace(Request.Form["studentclass"]) == true))
            {
                BookStudentsVM modelempty = new BookStudentsVM
                {
                    Students = DataService.getStudents(),
                    myBook = DataService.getBookbyID(ID),
                    StudentClasses = DataService.getStudentClasses(),
                    Borrow = DataService.getBorrowbyID(ID)
                };
                return View(modelempty);
            }
            // if both are selected
            else if((string.IsNullOrWhiteSpace(Request.Form["studentname"]) == false) && (string.IsNullOrWhiteSpace(Request.Form["studentclass"]) == false))
            {
                // Return matching statement for same class and name
                List<Student> newstudents1 = students.Where(x => x.Name.Contains(studentname) && x.Class == studentclass).ToList();
                BookStudentsVM model2 = new BookStudentsVM
                {
                    Students = newstudents1,
                    myBook = DataService.getBookbyID(ID),
                    StudentClasses = DataService.getStudentClasses(),
                    Borrow = DataService.getBorrowbyID(ID)
                };
                return View(model2);
            }
            //if student name is typed
            else if (string.IsNullOrWhiteSpace(Request.Form["studentname"]) == false)
            {
                // Adds the filtered list to filter
                List<Student> newstudents2 = students.Where(x => x.Name.Contains(studentname)).ToList();
                filtered.AddRange(newstudents2);
            }
            //if student class is selected
            else if (string.IsNullOrWhiteSpace(Request.Form["studentclass"]) == false)
            {
                // Adds the filtered list to filter
                List<Student> newstudents3 = students.Where(x => x.Class == studentclass).ToList();
                filtered.AddRange(newstudents3);
            }
            BookStudentsVM model = new BookStudentsVM
            {
                Students = filtered,
                myBook = DataService.getBookbyID(ID),
                StudentClasses = DataService.getStudentClasses(),
                Borrow = DataService.getBorrowbyID(ID),
            };
            return View(model); 
        }

        public ActionResult BorrowBook(int ID, int StudID)
        {
            // Initiates borrow
            DataService.borrowbook(ID, StudID);
            return RedirectToAction("Index", ID);
            //return RedirectToAction("BookDetails",ID);
        }
        public ActionResult ReturnBook(int ID, int BorrowID)
        {
            // Initiates returns
            DataService.returnbook(BorrowID);
            return RedirectToAction("Index", ID);
            //return RedirectToAction("BookDetails",ID);
        }
    }
}
