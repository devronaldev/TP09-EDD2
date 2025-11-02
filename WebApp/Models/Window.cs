using System.Collections.Generic;

namespace WebApp.Models;

/// <summary>
/// Represents a service window (guichê) responsible for calling and attending tickets.
/// </summary>
/// <remarks>
/// PT-BR: Representa uma janela de atendimento (guichê) responsável por chamar e atender senhas.
/// </remarks>
public class Window
{
    /// <summary>
    /// The unique identification number for the service window.
    /// </summary>
    /// <remarks>
    /// PT-BR: O número de identificação exclusivo para a janela de atendimento.
    /// </remarks>
    public int Id { get; set; }
    
    /// <summary>
    /// A private queue to store tickets that were successfully attended by this window.
    /// </summary>
    /// <remarks>
    /// PT-BR: Uma fila interna para armazenar as senhas que foram atendidas com sucesso por esta janela.
    /// </remarks>
    public Queue<Ticket> Tickets { get; set; }

    /// <summary>
    /// Initializes a new instance of the Window class with a specific ID.
    /// </summary>
    /// <param name="id">The unique ID number for the service window.</param>
    /// <remarks>
    /// PT-BR: Inicializa uma nova instância da classe Window com um ID específico.
    /// </remarks>
    public Window(int id)
    {
        Id = id;
        Tickets = new Queue<Ticket>();
    }

    /// <summary>
    /// Calls the next available ticket from the main queue, marks it as attended, and moves it to the window's internal queue.
    /// </summary>
    /// <param name="queue">The main Queue of waiting Tickets (e.g., from TicketLine).</param>
    /// <returns>True if a ticket was successfully called and attended; otherwise, False if the queue was empty.</returns>
    /// <remarks>
    /// PT-BR: Chama a próxima senha disponível da fila principal, marca o atendimento e move o ticket para a fila interna do guichê.
    /// </remarks>
    public bool Call(Queue<Ticket> queue)
    {
        if (queue.Count == 0)
        {
            return false;
        }
        var nextAttendance = queue.Dequeue();
        nextAttendance.Attend();
        Tickets.Enqueue(nextAttendance);
        return true;
    }
}