using System;

namespace elementium_backend.Models;

public interface Feedback
{
    public int Type { get; set; }
    public string Status { get; set; }
    public string? Message { get; set; }
    public object Body { get; set; }
}
