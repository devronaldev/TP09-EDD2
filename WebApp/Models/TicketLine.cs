using System.Collections.Generic;

namespace WebApp.Models;

/// <summary>
/// Represents the queue of service tickets for a single day/session.
/// </summary>
/// <remarks>
/// PT-BR: Representa a fila de tickets de atendimento para um único dia/sessão.
/// Esta classe não é thread-safe. É adequada para cenários de tela única ou
/// quando gerenciada estritamente como um Singleton na aplicação web.
/// </remarks>
public class TicketLine
{
    /// <summary>
    /// The internal counter used to determine the ID of the next ticket to be generated.
    /// It is private and resets to 1 with every new instance of TicketLine.
    /// </summary>
    /// <remarks>
    /// PT-BR: O contador interno para determinar o ID da próxima senha a ser gerada.
    /// É privado e é resetado para 1 a cada nova instância de TicketLine.
    /// </remarks>
    private int nextAttendance { get; set; }
    
    /// <summary>
    /// The collection of tickets currently in queue.
    /// Uses the FIFO (First-In, First-Out) model.
    /// </summary>
    /// <remarks>
    /// PT-BR: A coleção de tickets (senhas) atualmente em espera.
    /// Usa o modelo FIFO (First-In, First-Out).
    /// </remarks>
    public Queue<Ticket> Tickets { get; set; }

    /// <summary>
    /// Initializes a new instance of the ticket queue, resetting the ticket counter to 1.
    /// </summary>
    /// <remarks>
    /// PT-BR: Inicializa uma nova instância da fila de tickets, resetando o contador de senhas para 1.
    /// </remarks>
    public TicketLine()
    {
        Tickets = new Queue<Ticket>();
        nextAttendance = 1; 
    }

    /// <summary>
    /// Generates a new ticket, assigns the next available sequential ID,
    /// and adds it to the end of the waiting queue.
    /// </summary>
    /// <remarks>
    /// PT-BR: Gera um novo ticket, atribui o próximo ID sequencial disponível,
    /// e o adiciona ao final da fila de espera.
    /// </remarks>
    public void NewTicket()
    {
        Tickets.Enqueue(new Ticket(nextAttendance++));
    }
}