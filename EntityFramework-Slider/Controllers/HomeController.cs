using EntityFramework_Slider.Data;
using EntityFramework_Slider.Models;
using EntityFramework_Slider.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;

namespace EntityFramework_Slider.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {


            //Linkque querileri =Blogs.Where(m => !m.SoftDelete)-Silinimeyen bloglari ver

            //List miras alir IIEnumerabldan.List-in methodlari var IEnumerable-un yoxdu.Datani viewa IEnumerable kimi gondermek yaxsdidir cunku- 
            //elave methodlar yoxdu daha suretli isleyir

            //IQueryable-query yaradiriq RAMda saxlayiriq hele data getirmirik
            //IQueryable<Slider> slide = _context.Sliders.AsQueryable();
            //sonra serte uyqun datani getiririk.ToList()-yazdiqda request gedir DataBazaya
            //List<Slider> query = slide.Where(m => m.Id >5).ToList();

            //List<int> nums = new List<int>() { 1, 2, 4, 5, 6 };
            //FirstOrDefault()--sertde gondermek olur ,serte uyqun datadan bir necedenedise 1-cini gorsedir,yoxdusa default deyerini gorsedir
            //var res = nums.FirstOrDefault(m => m ==3);

            //First()--varsa data serte uyqunun verir,yoxdusa exception cixarir
            //var res2 = nums.First(m => m == 3);

            //SingleOrDefault()--serte uyqun data  birdene varsa verir,bir necedenedise exception verir,yoxdusa data default deyerin verir
            //Single()--serte uyqun data  birdene varsa verir,bir necedenedise exception verir,yoxdusa exception verir
            //var res = nums.SingleOrDefault(m => m == 3);          
            //ViewBag.num = res;



            List<Slider> sliders = await _context.Sliders.ToListAsync();

            SliderInfo sliderInfo = await _context.SliderInfos.FirstOrDefaultAsync();

            IEnumerable<Blog> blogs = await _context.Blogs.Where(m=>!m.SoftDelete).ToListAsync();

            IEnumerable<Category> categories = await _context.Categories.Where(m => !m.SoftDelete).ToListAsync();


            //Inlude()--Relation  qurduqumuz tablerda istifade edirik.-Tablin icindeki basqa table catmaq istirsense.Many olan terefe.

            IEnumerable<Product> products = await _context.Products.Include(m=>m.Images).Where(m => !m.SoftDelete).ToListAsync();

            About abouts = await _context.Abouts.Include(m => m.Adventages).FirstOrDefaultAsync();

            IEnumerable<Experts> experts = await _context.Experts.Where(m => !m.SoftDelete).ToListAsync();

            ExpertsHeader expertsheaders = await _context.ExpertsHeaders.FirstOrDefaultAsync();

            Subscribe subscribs = await _context.Subscribs.FirstOrDefaultAsync();

            BlogHeader blogheaders = await _context.BlogHeaders.FirstOrDefaultAsync();
            IEnumerable<Say> says = await _context.Says.Where(m => !m.SoftDelete).ToListAsync();

            IEnumerable<Instagram> instagrams = await _context.Instagrams.Where(m => !m.SoftDelete).ToListAsync();

            HomeVM model = new()
            {
                Sliders = sliders,
                SliderInfo = sliderInfo,
                Blogs = blogs,
                Categories = categories,
                Products = products,
                Abouts = abouts,
                Experts =experts,
                ExpertsHeaders = expertsheaders,
                Subscribs =subscribs,
                BlogHeaders=blogheaders,
                Says= says,
                Instagrams= instagrams
            };

            return View(model);
        }
    }
}