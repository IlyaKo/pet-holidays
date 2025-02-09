using LosTomates.PetHolidays.RabbitMQ.Core.Services.Rabbit;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace LosTomates.PetHolidays.RabbitMQ.Web.Controllers;

[ApiController]
[Route("api/rabbit")]
public class RabbitController(IRpcClient rpcClient) : Controller
{
    

    /// <summary>
    /// Publica un mensaje en RabbitMQ.
    /// </summary>
    /// <param name="exchange">Nombre del exchange.</param>
    /// <param name="routingKey">Clave de enrutamiento.</param>
    /// <param name="message">Mensaje a enviar.</param>
    [HttpPost("publish")]
    public async Task<IActionResult> PublishMessage([FromBody] JsonNode message, [FromQuery] string queue)
    {
        var call = await rpcClient.Call(message.ToString(), queue);
        return Ok(call);
    }

    /// <summary>
    /// Inicia un consumidor de RabbitMQ.
    /// </summary>
    /// <param name="exchange">Nombre del exchange.</param>
    /// <param name="queue">Nombre de la cola.</param>
    /// <param name="routingKey">Clave de enrutamiento.</param>
    //[HttpPost("consume/start")]
    //public async Task<IActionResult> StartConsumer([FromQuery] string exchange, [FromQuery] string queue, [FromQuery] string routingKey)
    //{
    //    if (string.IsNullOrEmpty(exchange) || string.IsNullOrEmpty(queue) || string.IsNullOrEmpty(routingKey))
    //    {
    //        return BadRequest("Exchange, queue y routingKey son obligatorios.");
    //    }

    //    await _consumer.ConsumeAsync(exchange, queue, routingKey);
    //    return Ok($"Consumidor iniciado en la cola '{queue}'.");
    //}

    /// <summary>
    /// Detiene el consumidor de RabbitMQ.
    /// </summary>
    //[HttpPost("consume/stop")]
    //public async Task<IActionResult> StopConsumer()
    //{
    //    await _consumer.StopConsumingAsync();
    //    return Ok("Consumidor detenido.");
    //}
}
