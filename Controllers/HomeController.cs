using System.Diagnostics;
using AnalisisSentimental;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using trabajo_final_grupo_verde.Data;
using trabajo_final_grupo_verde.Models;
using trabajo_final_grupo_verde.Models.Entity;

namespace trabajo_final_grupo_verde.Controllers;
public class HomeController : Controller
{

    private readonly ApplicationDbContext _context;
    private readonly ILogger<HomeController> _logger;

    private readonly IMyEmailSender _emailSender;

    private readonly PredictionEnginePool<MLModel1.ModelInput, MLModel1.ModelOutput> _predictionEnginePool;

    private readonly MLController _mLController;

    public HomeController(ILogger<HomeController> logger,
        ApplicationDbContext context, IMyEmailSender emailSender, PredictionEnginePool<MLModel1.ModelInput, MLModel1.ModelOutput> predictionEnginePool)
    {
        _logger = logger;

        /* lineas agregadas */
        _context = context;

        _emailSender = emailSender;

        _predictionEnginePool = predictionEnginePool;

    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult PreguntasFrecuentes()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Contacto()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Contacto objContacto)
    {

        MLModel1.ModelInput modelInput = new MLModel1.ModelInput()
        {
            SENTIMIENTO_TEXT = objContacto.Mensaje
        };

        MLModel1.ModelOutput prediction = _predictionEnginePool.Predict(modelInput);
        ViewData["Sentimiento"] = prediction.PredictedLabel;
        ViewData["Score"] = prediction.Score[1];

        if(prediction.Score[1] >=0.5){
            objContacto.Comentario = "Positivo";
        }

        else{
            objContacto.Comentario = "Negativo";
        }


        _context.Add(objContacto);
        await _context.SaveChangesAsync();

        var message = $"Estimado(a) {objContacto.Nombre}, te estaremos contactando pronto";
        TempData["MessageCONTACTO"] = message;
        var message1 = $@"
            Estimado(a) {objContacto.Nombre},

            ¡Gracias por ponerte en contacto con nosotros!

            Hemos recibido tu solicitud y uno de nuestros representantes se pondrá en contacto contigo a la brevedad. 
            Valoramos tu interés y nos esforzamos por responder todas las consultas lo más rápido posible.

            Tu mensaje fue:
            {objContacto.Mensaje}

            Tu Número Telefónico fue: {objContacto.Phone}
            Tu Correo electronico fue: {objContacto.Email}
            Tu prediccion de tu comentario fue : {objContacto.Comentario}

            Mientras tanto, te invitamos a explorar nuestro sitio web o nuestras redes sociales para obtener más información sobre nuestros productos y servicios.

            ¡Gracias por elegirnos!

            Saludos cordiales,

            [La Empresa de vinos Vilife]
        ";



        //await _emailSender.SendEmailAsync(objContacto.Email, "Gracias por contactarnos", message);
        await _emailSender.SendEmailAsync(objContacto.Email, "" + objContacto.Asunto, message1);
        return View("~/Views/Home/Contacto.cshtml");
    }




}
