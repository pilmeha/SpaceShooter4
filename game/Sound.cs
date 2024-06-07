using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace SpaceShooter3.game
{
    internal class Sound
    {
        public static SoundEffect FireSound { get; private set; }
        public static SoundEffect BoomSound { get; private set; }
        public static SoundEffect MenuSound { get; private set; }
        public static SoundEffect EnterSound { get; private set; }
        public static SoundEffect IntersectedSound { get; private set; }
        public static SoundEffect BottleSound { get; private set; }
        public static SoundEffect BuySound { get; private set; }
        public static Song GameSong { get; private set; }

        public static void Load(ContentManager content)
        {
            FireSound = content.Load<SoundEffect>("Sounds/fireSound");
            BoomSound = content.Load<SoundEffect>("Sounds/boomSound");
            MenuSound = content.Load<SoundEffect>("Sounds/menuSound");
            EnterSound = content.Load<SoundEffect>("Sounds/enterSound");
            IntersectedSound = content.Load<SoundEffect>("Sounds/intersectedSound");
            BottleSound = content.Load<SoundEffect>("Sounds/heartSound");
            BuySound = content.Load<SoundEffect>("Sounds/buySound");

            GameSong = content.Load<Song>("Sounds/KavinskyNightcall");
        }
    }
}
