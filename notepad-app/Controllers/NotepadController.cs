using Microsoft.AspNetCore.Mvc;
using notepad_app.Helper;
using System.Data;

namespace notepad_app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotepadController : ControllerBase
    {

        private readonly ILogger<NotepadController> _logger;

        public NotepadController(ILogger<NotepadController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Notepad> Get()
        {
            List<Notepad> notepad = new List<Notepad>();
            Tuple<DataTable, string> results = DBHelper.ConnectDB();
            DataTable notepadData = results.Item1;
            string errMessage = results.Item2;

            if (!string.IsNullOrEmpty(errMessage))
            {
                return notepad;
            }


            foreach(DataRow row in notepadData.Rows)
            {
                notepad.Add(new Notepad {
                    Date = string.IsNullOrEmpty(row[3].ToString())? DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss tt") : ((DateTime)row[3]).ToString("MM/dd/yyyy HH:mm:ss tt"),
                    Title = row[1].ToString(),
                    Summary = row[2].ToString()
                });
            }
            return notepad;
        }

        [HttpPost]
        public void Post([FromBody]Notepad notepad)
        {   
            DBHelper.CreateNewNote
            ( 
                String.IsNullOrEmpty(notepad.Title)?"":notepad.Title,
                String.IsNullOrEmpty(notepad.Summary)?"":notepad.Summary
            );
        }
    }
}