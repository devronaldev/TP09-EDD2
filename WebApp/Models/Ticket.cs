using System.Globalization;

namespace WebApp.Models;

/// <summary>
/// Represents a single service ticket, recording its creation and attendance times in UTC.
/// </summary>
/// <remarks>
/// PT-BR: Representa uma única senha de atendimento, registrando seus horários de criação e atendimento em UTC (Tempo Universal Coordenado).
/// </remarks>
public class Ticket
{
    /// <summary>
    /// The unique sequential identification number for the ticket.
    /// </summary>
    /// <remarks>
    /// PT-BR: O número de identificação sequencial único para a senha.
    /// </remarks>
    public int Id { get; set; }
    
    /// <summary>
    /// The date and time (in UTC) when the ticket was created and added to the queue.
    /// </summary>
    /// <remarks>
    /// PT-BR: A data e hora (em UTC) em que a senha foi criada e adicionada à fila.
    /// </remarks>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// The date and time (in UTC) when the ticket was attended/called by the service agent.
    /// </summary>
    /// <remarks>
    /// PT-BR: A data e hora (em UTC) em que a senha foi atendida/chamada pelo atendente.
    /// </remarks>
    public DateTime AttendAt { get; set; }
    
    /// <summary>
    /// Static read-only CultureInfo object set to Brazilian Portuguese (pt-BR) for formatting purposes.
    /// </summary>
    /// <remarks>
    /// PT-BR: Objeto estático e somente leitura de CultureInfo configurado para Português do Brasil (pt-BR) para fins de formatação.
    /// </remarks>
    private static readonly CultureInfo PtBrCulture = new CultureInfo("pt-BR");

    /// <summary>
    /// Initializes a new instance of the Ticket class with a specific ID and registers the creation time in UTC.
    /// </summary>
    /// <param name="id">The sequential ID number for the new ticket.</param>
    /// <remarks>
    /// PT-BR: Inicializa uma nova instância da classe Ticket com um ID específico e registra o horário de criação em UTC.
    /// </remarks>
    public Ticket(int id)
    {
        Id = id;
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Returns the basic ticket information (ID and creation time), formatted in Brasília local time.
    /// </summary>
    /// <returns>A formatted string containing the ticket ID, date, and creation time.</returns>
    /// <remarks>
    /// PT-BR: Retorna as informações básicas da senha (ID e horário de criação), formatadas no horário local de Brasília.
    /// </remarks>
    public string PartialData() => $"Senha: {Id} - {FormatUtcToPtBr(CreatedAt)}";
    
    /// <summary>
    /// Returns the full ticket information (creation and attendance times), formatted in Brasília local time.
    /// </summary>
    /// <returns>A formatted string containing all ticket details. Includes a 'not attended' message if AttendAt is not set.</returns>
    /// <remarks>
    /// PT-BR: Retorna as informações completas da senha (horários de criação e atendimento), formatadas no horário local de Brasília. Inclui uma mensagem 'não atendido' se AttendAt não estiver definido.
    /// </remarks>
    public string FullData()
    {
        if (AttendAt == DateTime.MinValue)
        {
            return PartialData() + " - (Ainda não atendido)";
        } 
        return PartialData() + $" || {FormatUtcToPtBr(AttendAt)}";
    }
    
    /// <summary>
    /// Sets the attendance time for the ticket to the current UTC time.
    /// </summary>
    /// <remarks>
    /// PT-BR: Define o horário de atendimento para a senha, usando o horário UTC atual.
    /// </remarks>
    public void Attend() => AttendAt = DateTime.UtcNow;
    
    /// <summary>
    /// Converts a UTC time value to Brasília local time and formats it using the Brazilian standard (dd/MM/yyyy - HH:mm:ss).
    /// </summary>
    /// <param name="utcTime">The DateTime value in UTC to be formatted.</param>
    /// <returns>The formatted string representing the local date and time.</returns>
    /// <remarks>
    /// PT-BR: Converte um valor de horário UTC para o horário local de Brasília e o formata usando o padrão brasileiro (dd/MM/yyyy - HH:mm:ss).
    /// </remarks>
    private string FormatUtcToPtBr(DateTime utcTime)
    {
        TimeZoneInfo brazilTz = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo"); 
        DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, brazilTz);
        string data = localTime.ToString("dd/MM/yyyy", PtBrCulture);
        string hora = localTime.ToString("HH:mm:ss", PtBrCulture);
        
        return $"{data} - {hora}";
    }
}