using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace u21570869_HW05.Models
{
    public class DataService
    {
        public DataService()
        {

        }
        string connectionstring = "Data Source=.\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;";

        //Gets book lists
        public List<Book> getBooks()
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            List<Book> Books = new List<Book>();
            try
            {
                connection.Open();
                SqlCommand query = new SqlCommand("Select * From books", connection);
                SqlDataReader myreader = query.ExecuteReader();
                while (myreader.Read())
                {
                    Book book = new Book();
                    book.ID = Convert.ToInt32(myreader["bookId"]);
                    book.Name = Convert.ToString(myreader["name"]);
                    book.PageCount = Convert.ToInt32(myreader["pagecount"]);
                    book.Point = Convert.ToInt32(myreader["point"]);
                    book.Author = getAuthorbyID(Convert.ToInt32(myreader["authorId"]));
                    book.BookType = getBookTypebyID(Convert.ToInt32(myreader["typeId"]));
                    book.Borrows = getbookborrowsnum(Convert.ToInt32(myreader["bookId"]));
                    book.Status = getbookstatus(book.ID);
                    Books.Add(book);
                }
            }
            catch (Exception)
            {
         
            }
            finally
            {
                connection.Close();
            }
            return Books;
        }

        //Gets borrow nook list
        public List<Borrow> getBorrowedBook(int ID)
        {
            List<Borrow> items = new List<Borrow>();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand query = new SqlCommand("select * from borrows where bookId = " + ID, connection);
            SqlDataReader myreader = query.ExecuteReader();
            while (myreader.Read())
            {
                Borrow borrow = new Borrow();
                borrow.ID = Convert.ToInt32(myreader["borrowId"]);
                borrow.Book = getBookbyID(Convert.ToInt32(myreader["bookId"]));
                borrow.Student = getStudentbyID(Convert.ToInt32(myreader["studentId"]));
                borrow.TakenDate = Convert.ToString(myreader["takenDate"]);
                borrow.BroughtDate = Convert.ToString(myreader["broughtDate"]);
                items.Add(borrow);
            }
            connection.Close(); 
            return items;
        }

        //Gets author record by its ID
        public Author getAuthorbyID(int ID)
        {
            Author author = new Author();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand query = new SqlCommand("select * from authors where authorId = " + ID, connection);
            SqlDataReader myreader = query.ExecuteReader();
            while (myreader.Read())
            {
                author.ID = Convert.ToInt32(myreader["authorId"]);
                author.Name = Convert.ToString(myreader["name"]);
                author.Surname = Convert.ToString(myreader["surname"]);
            }
            connection.Close();
            return author;
        }

        //Gets booktype record by its ID
        public BookType getBookTypebyID(int ID)
        {
            BookType type = new BookType();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand query = new SqlCommand("select * from types where typeId = " + ID, connection);
            SqlDataReader myreader = query.ExecuteReader();
            while (myreader.Read())
            {
                type.ID = Convert.ToInt32(myreader["typeId"]);
                type.Name = Convert.ToString(myreader["name"]);
            }
            connection.Close();
            return type;
        }

        //Gets student record by its ID
        public Student getStudentbyID(int ID)
        {
            Student student = new Student();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand query = new SqlCommand("select * from students where studentId = " + ID, connection);
            SqlDataReader myreader = query.ExecuteReader();
            while (myreader.Read())
            {
                student.ID = Convert.ToInt32(myreader["studentId"]);
                student.Name = Convert.ToString(myreader["name"]);
                student.Surname = Convert.ToString(myreader["surname"]);
                student.Point = Convert.ToString(myreader["point"]);
                student.BirthDate = Convert.ToString(myreader["birthdate"]);
                student.Class = Convert.ToString(myreader["class"]);
                student.Gender = Convert.ToString(myreader["gender"]);
            }
            connection.Close();
            return student;
        }

        //Gets book record by its ID
        public Book getBookbyID(int ID)
        {
            Book book = new Book();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand query = new SqlCommand("Select * From books where bookId = " + ID, connection);
            SqlDataReader myreader = query.ExecuteReader();
            while (myreader.Read())
            {
                book.ID = Convert.ToInt32(myreader["bookId"]);
                book.Name = Convert.ToString(myreader["name"]);
                book.PageCount = Convert.ToInt32(myreader["pagecount"]);
                book.Point = Convert.ToInt32(myreader["point"]);
                book.Author = getAuthorbyID(Convert.ToInt32(myreader["authorId"]));
                book.BookType = getBookTypebyID(Convert.ToInt32(myreader["typeId"]));
                book.Borrows = getbookborrowsnum(Convert.ToInt32(myreader["bookId"]));
                book.Status = getbookstatus(book.ID);
            }
            connection.Close();
            return book;
        }

        //Gets borrow record by its ID
        public Borrow getBorrowbyID(int ID)
        {
            Borrow borrow = new Borrow();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            //Gets max first to apply in the where statement
            SqlCommand query = new SqlCommand("SELECT * FROM borrows WHERE takenDate = (select MAX(takenDate) From borrows where bookId = " + ID + ")", connection);
            SqlDataReader myreader = query.ExecuteReader();
            while (myreader.Read())
            {
                borrow.ID = Convert.ToInt32(myreader["borrowId"]);
                borrow.TakenDate = Convert.ToString(myreader["takenDate"]);
                borrow.BroughtDate = Convert.ToString(myreader["broughtDate"]);
                borrow.Book= getBookbyID(Convert.ToInt32(myreader["bookId"]));
                borrow.Student = getStudentbyID(Convert.ToInt32(myreader["studentId"]));
            }
            connection.Close();
            return borrow;
        }

        // Gets list of students for the student view
        public List<Student> getStudents()
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            List<Student> students = new List<Student>();
            try
            {
                connection.Open();
                SqlCommand query = new SqlCommand("SELECT * FROM students", connection);
                SqlDataReader myreader = query.ExecuteReader();
                while (myreader.Read())
                {
                    Student student = new Student();
                    student.ID = Convert.ToInt32(myreader["studentId"]);
                    student.Name = Convert.ToString(myreader["name"]);
                    student.Surname = Convert.ToString(myreader["surname"]);
                    student.Point = Convert.ToString(myreader["point"]);
                    student.BirthDate = Convert.ToString(myreader["birthdate"]);
                    student.Class = Convert.ToString(myreader["class"]);
                    student.Gender = Convert.ToString(myreader["gender"]);
                    //student = getStudentbyID(Convert.ToInt32(myreader["studentId"]));
                    students.Add(student);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                connection.Close();
            }
            return students;
        }

        // Gets the amount of borrows for the BookDetails view
        public int getbookborrowsnum(int ID)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            int num = 0;
            try
            {
                connection.Open();
                SqlCommand query = new SqlCommand("SELECT COUNT(*) as borrows FROM borrows WHERE bookId = " + ID, connection);
                SqlDataReader myreader = query.ExecuteReader();
                while (myreader.Read())
                {
                    num = Convert.ToInt32(myreader["borrows"]);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                connection.Close();
            }
            return num;
        }
        
        // Gets the book status
        public string getbookstatus(int ID)
        {
            string status = "";
            string broughtdate;
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand query = new SqlCommand("SELECT bookId, takenDate, broughtDate FROM borrows WHERE takenDate = (select MAX(takenDate) From borrows where bookId = " + ID + ")", connection);
            SqlDataReader myreader = query.ExecuteReader();
            while (myreader.Read())
            {
                
                //Checks the broughtdate for null and assigns status according to it
                if (myreader.IsDBNull(2))
                {
                    status = "Out";
                }
                else
                {
                    status = "Available";
                }
            }
            connection.Close();
            return status;
        }

        //Gets author name for filter dropdown
        public List<string> getAuthorNames()
        {
            List<string> names = new List<string>();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand query = new SqlCommand("SELECT name FROM authors", connection);
            SqlDataReader myreader = query.ExecuteReader();
            while (myreader.Read())
            {
                names.Add(Convert.ToString(myreader["name"]));
            }
            connection.Close();
            return names;
        }

        //Gets booktype name for filter dropdown
        public List<string> getTypeNames()
        {
            List<string> types = new List<string>();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand query = new SqlCommand("SELECT name FROM types", connection);
            SqlDataReader myreader = query.ExecuteReader();
            while (myreader.Read())
            {
                types.Add(Convert.ToString(myreader["name"]));
            }
            connection.Close();
            return types;
        }

        //Gets studentclasses for filter dropdown
        public List<string> getStudentClasses()
        {
            List<string> classes = new List<string>();
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand query = new SqlCommand("SELECT DISTINCT class FROM students", connection);
            SqlDataReader myreader = query.ExecuteReader();
            while (myreader.Read())
            {
                classes.Add(Convert.ToString(myreader["class"]));
            }
            connection.Close();
            return classes;
        }

        // New record for the insert
        public void borrowbook(int ID, int StudID)
        {
            //Formate Date so it can go in SQL
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            SqlCommand query = new SqlCommand("INSERT INTO borrows (studentId, bookId, takenDate, broughtDate) Values(" + StudID + ", " + ID + ", CAST('" + sqlFormattedDate + "' AS DATETIME), " + " NULL)", connection);
            query.ExecuteNonQuery();
            connection.Close();
        }

        // Returns the book by updating the broughtDate
        public void returnbook(int BorrowID)
        {
            //Formate Date so it can go in SQL
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand query = new SqlCommand("UPDATE borrows set broughtDate = CAST('" + sqlFormattedDate + "' AS DATETIME) WHERE borrowId = " + BorrowID, connection);
            query.ExecuteNonQuery(); 
            connection.Close();
        }

    }
}