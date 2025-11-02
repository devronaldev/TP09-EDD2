using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class QueueController : Controller
{
    private static ServiceWindows _queueManager { get; } = InitializeService();
    private static ServiceWindows InitializeService()
    {
        var manager = new ServiceWindows();
        manager.AddWindow(new Window(1)); 
        return manager;
    }
    
    public IActionResult Index(int selectedWindowId = 1)
    {
        var manager = _queueManager;
        
        ViewData["WaitingTickets"] = manager.TicketLine.Tickets.ToArray();
        ViewData["AttendedTickets"] = manager.Windows.SelectMany(w => w.Tickets).ToArray();
        ViewData["WindowCount"] = manager.Windows.Count;
        ViewData["SelectedWindowId"] = selectedWindowId;
        return View("~/Views/Home/Index.cshtml");
    }
    
    // ... (Generate, Call, AddWindow continuam iguais)
    
    [HttpPost]
    public IActionResult Generate()
    {
        _queueManager.TicketLine.NewTicket();
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    public IActionResult Call(int selectedWindowId)
    {
        if (selectedWindowId <= 0)
        {
            return RedirectToAction(nameof(Index));
        }
        
        var window = _queueManager.Windows.FirstOrDefault(x => x.Id == selectedWindowId);
            
        if (window != null)
        {
            window.Call(_queueManager.TicketLine.Tickets);
        }
        return RedirectToAction(nameof(Index), new { selectedWindowId = selectedWindowId });
    }
    
    [HttpPost]
    public IActionResult AddWindow()
    {
        var manager = _queueManager;
        int nextId = manager.Windows.Any() 
            ? manager.Windows.Max(w => w.Id) + 1 
            : 1;

        manager.AddWindow(new Window(nextId));
        return RedirectToAction(nameof(Index), new { selectedWindowId = nextId });
    }
}