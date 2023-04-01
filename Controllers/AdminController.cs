using FirstApp.Data;
using FirstApp.Models;
using FirstApp.Models.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstApp.Controllers
{
    public class AdminController : Controller
    {    
        private readonly EmployeeDBContext employeeDBContext;

        public AdminController(EmployeeDBContext employeeDBContext)
        {
            this.employeeDBContext = employeeDBContext; // dengan membuat ini, bisa private readonly dan connect databse
        }

        // Membuat fungsi get untuk menampilkan ke layar
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var empolyees = await employeeDBContext.Employees.ToListAsync();
            return View(empolyees);
        }
        // buat fungsi untuk menambahkan karyawan
        // setelah itu kita dpat menampilkan nya ke layar
        //sdsdaasd 
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        //properti ini akan require paramater
        //Add(AddEmployeeViewModel addEmployeeRequest), menerima method yang lalu diberi nama nya
        {
            //1. harus mengubah dari AddEmployeeViewModel -> AddEmployessController
            var employee = new Employee()
            {
                EmployeeId = Guid.NewGuid(), //karena ingin menetepkan nilai id, maka require langsung dari guid
                Name = addEmployeeRequest.Name,
                EmailId = addEmployeeRequest.Email,
                userPassword=addEmployeeRequest.userPassword,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth,

                //setelah membuat ini, saat nya memasukkan ke dlam database
            };

            await employeeDBContext.Employees.AddAsync(employee);
            await employeeDBContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await employeeDBContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);

            if(employee != null)
            {
                // membuat variabel yang akan mengarahkan ke UpdateEmployeeViewModel
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.EmployeeId, 
                    Name = employee.Name,
                    Email = employee.EmailId,
                    userPassword = employee.userPassword,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth,
                };
                return await Task.Run(() => View("View", viewModel));
            }

            return View(employee); 
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        { 
            // mengambil data dari databse terlebih dahalu
            var employee = await employeeDBContext.Employees.FindAsync(model.Id);

            if (employee != null)
            { 
                employee.Name = model.Name;
                employee.EmailId = model.Email;
                employee.userPassword = model.userPassword;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;

                await employeeDBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await employeeDBContext.Employees.FindAsync(model.Id);  

            if (employee != null)
            {
                employeeDBContext.Employees.Remove(employee);

                await employeeDBContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("index");
        }
    }
} 
