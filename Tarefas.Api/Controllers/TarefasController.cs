using Microsoft.AspNetCore.Mvc;
using Tarefas.API.Models;

namespace Tarefas.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TarefasController : ControllerBase
{
    
    private readonly ILogger<TarefasController> _logger;
   	private readonly IMessageProducer _messageProducer;

    public static readonly List<Tarefa> _tarefas = new();

    public TarefasController(ILogger<TarefasController> logger, IMessageProducer messageProducer)
    {
        _logger = logger;
        _messageProducer = messageProducer;
    }
    [HttpPost]
    public IActionResult CreatingTarefa(Tarefa newTarefa)
	{
	 if(!ModelState.IsValid) return BadRequest();
	 
	 _tarefas.Add(newTarefa);
	 
	 _messageProducer.SendingMessage<Tarefa>(newTarefa);
	 
	 return Ok();
    }
}