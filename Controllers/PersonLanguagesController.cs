using AliKuli.Exceptions;
using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.Models.People;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bearer.Controllers
{
    public class PersonLanguagesController : AbstractController<PersonLanguage>
    {
        public PersonLanguagesController():
            base(new PersonLanguageDAL(SetApplicationDbContext(), GetUser()))
        {

        }


        private static ApplicationDbContext SetApplicationDbContext()
        {
            return new ApplicationDbContext();
        }

        private static string GetUser()
        {
            StringBuilder sb = new StringBuilder();
            string s = AliKuli.GetUser.Name(null);
            sb.Append(s);
            return s.ToString();
        }

        //---------------------------------------------------------------------


        public override async Task<ActionResult> Create([Bind(Include = "Name,Comment,CreatedDateStarted,LanguageId,PersonId")] PersonLanguage entity)
        {
            if (ModelState.IsValid)
            {

                try
                {
                        
                    repo.Create(entity);
                    await repo.SaveAsync();
                    return RedirectToIndexActionHelper(string.Format("The entity has been saved"));

                }
                catch (NoDuplicateException e)
                {
                    ModelState.AddModelError("", e.Message);
                }
                catch (Exception e)
                {
                    string message = MakeErrorMesage("Unable to create.", e);
                    ModelState.AddModelError("", message);
                }
            }
            //ModelState.AddModelError("", string.Format("There was an error. The entity did not get saved." ));
            return View(entity);
        }
    }
}
