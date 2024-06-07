namespace SpaceShooter3.game
{
    internal class Container
    {
        public Line Width { get; set; }
        public Line Height { get; set; }
        public Container(Line width, Line height)
        {
            Width = width;
            Height = height;
        }
    }
}
