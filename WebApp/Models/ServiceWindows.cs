namespace WebApp.Models;

/// <summary>
/// Central manager for the service system, coordinating the waiting line and the service windows.
/// </summary>
/// <remarks>
/// PT-BR: Gerenciador central do sistema de atendimento, coordenando a fila de espera e as janelas de serviço.
/// </remarks>
public class ServiceWindows // Usando o nome ServiceController
{
    /// <summary>
    /// The collection of active service windows (guichês).
    /// </summary>
    /// <remarks>
    /// PT-BR: A coleção de janelas de serviço (guichês) ativas.
    /// </remarks>
    public List<Window> Windows { get; set; } = new List<Window>();
    
    /// <summary>
    /// The main queue for all waiting tickets.
    /// </summary>
    /// <remarks>
    /// PT-BR: A fila principal para todas as senhas em espera.
    /// </remarks>
    public TicketLine TicketLine { get; set; } = new TicketLine();

    /// <summary>
    /// Adds a new service window to the manager's collection.
    /// </summary>
    /// <param name="window">The Window object to be added.</param>
    /// <remarks>
    /// PT-BR: Adiciona uma nova janela de serviço (guichê) à coleção do gerenciador.
    /// </remarks>
    public void AddWindow(Window window)
    {
        Windows.Add(window);
    }

    /// <summary>
    /// Initiates the creation of a new ticket and places it in the waiting line.
    /// </summary>
    /// <remarks>
    /// PT-BR: Inicia a criação de uma nova senha e a coloca na fila de espera.
    /// </remarks>
    public void IssueNewTicket()
    {
        TicketLine.NewTicket();
    }
    
    /// <summary>
    /// Directs a specific service window to call the next ticket from the main queue.
    /// </summary>
    /// <param name="windowId">The ID of the window making the call.</param>
    /// <returns>True if a ticket was successfully called and processed; otherwise, False if the queue was empty.</returns>
    /// <exception cref="System.ArgumentException">Thrown if the specified window ID is not found.</exception>
    /// <remarks>
    /// PT-BR: Direciona uma janela de serviço específica para chamar a próxima senha da fila principal. Lança uma exceção se o ID da janela não for encontrado.
    /// </remarks>
    public bool CallNextTicket(int windowId)
    {
        var window = Windows.FirstOrDefault(x => x.Id == windowId);
        
        if (window == null)
        {
            throw new ArgumentException($"O Guichê com ID {windowId} não foi encontrado.");
        }
        
        return window.Call(TicketLine.Tickets);
    }
}