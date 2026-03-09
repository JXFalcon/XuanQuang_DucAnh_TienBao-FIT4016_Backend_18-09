public class Player
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ImagePath { get; set; } // Ảnh cầu thủ
    public string? Nationality { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Position { get; set; } // Tiền đạo, Tiền vệ...
    public int ShirtNumber { get; set; }
    
    // Chỉ số sức mạnh (Stats)
    public int Pace { get; set; }
    public int Shooting { get; set; }
    public int Passing { get; set; }
    public int Dribbling { get; set; }
    public int Defending { get; set; }
    public int Physical { get; set; }

    // Thông tin câu lạc bộ
    public int TeamId { get; set; }
    
}