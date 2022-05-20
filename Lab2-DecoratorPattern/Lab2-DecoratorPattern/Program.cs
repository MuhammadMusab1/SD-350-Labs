/*
Create a simple game where a ‘Player’ can use a ‘Weapon’ to kill an ‘Enemy.' If the ‘Enemy’ is a certain type, 
like a "boss" or "giant enemy," then the user needs to upgrade his weapon and decorate it with a special ability.

Create multiple special abilities. Each one should only allow the player to "win" against one type of enemy.

In the main function, try to create a simple simulation for facing a special enemy and asking the
user to press a button to get an upgrade so they in order to win the fight.
 */
Weapon myWeapon = new Havoc();
Player player1 = new Player()
{
    PlayerName = "ACEU",
    Weapon = myWeapon
};
Enemy enemy = new Wraith();
Game game = new Game()
{
    Player = player1,
    Weapon = myWeapon,
    Enemy = enemy
};
game.StartGame();
Console.ReadLine();

public class Game
{
    public Player Player { get; set; }
    public Enemy Enemy { get; set; }
    public Weapon Weapon { get; set; }
    public void StartGame()
    {
        Console.WriteLine($"Fighting Enemy: {Enemy.GetType()}, {Enemy.EnemyType}");
        Console.WriteLine($"Weapon: {Weapon.GetType()}, a {Weapon.WeaponType}");
        if(Enemy.GetType().ToString() == "Wraith" || Enemy.GetType().ToString() == "Octane")
        {
            //Can't win without updagrade
            AskForUpgrade();
            DetermineWhoWins();
            Console.WriteLine("Game Over");
        }
        else
        {
            DetermineWhoWins();
            Console.WriteLine("Game Over");
        }
    }
    private void AskForUpgrade()
    {
        Console.WriteLine();
        Console.WriteLine("Enemy is too strong. Press 1 for upgrade");
        string num = Console.ReadLine();
        while (String.IsNullOrEmpty(num) || int.Parse(num) != 1)
        {
            Console.WriteLine("Bruh! I said Enemy is too strong. Press 1 for upgrade");
            num = Console.ReadLine();
        }
        Console.WriteLine();
        Console.WriteLine($"{Weapon.GetType()} is being upgraded");

        if(Weapon.GetType().ToString() ==  "Wingman")
        {
            Weapon = new ExtendedMag(Weapon);
            Console.WriteLine();
            Console.WriteLine($"Weapon now has an {Weapon.GetType()}");
        }else if(Weapon.GetType().ToString() == "Havoc")
        {
            Weapon = new TurboCharger(Weapon);
            Console.WriteLine();
            Console.WriteLine($"Weapon now has a {Weapon.GetType()}");
        }
    }
    private void DetermineWhoWins()
    {
        Console.WriteLine();
        if (Enemy.GetType().ToString() == "Wraith" && Weapon.GetType().ToString() == "ExtendedMag")
        {
            Console.WriteLine($"{Enemy.GetType()} is defeated after upgrade");
        } else if(Enemy.EnemyType == "Interdimensional Jumper")
        {
            Console.WriteLine($"Couldn't defeat {Enemy.GetType()} even after the upgrade. She is too strong");
        }
        if (Enemy.GetType().ToString() == "Octane" && Weapon.GetType().ToString() == "TurboCharger")
        {
            Console.WriteLine($"{Enemy.GetType()} is defeated after upgrade");
        }
        else if (Enemy.GetType().ToString() == "Octane")
        {
            Console.WriteLine($"Couldn't defeat {Enemy.GetType()} even after the upgrade. He is too strong");
        }
        if((Enemy.GetType().ToString() == "Noob")) 
        {
            Console.WriteLine($"Enemy {Enemy.GetType()} is defeated no upgrades were needed");
        }
    }
}
//Weapon
public abstract class Weapon
{
    public string WeaponType { get; set; }
}
public class Wingman : Weapon
{
    public Wingman()
    {
        WeaponType = "Pistol";
    }
}
public class Havoc : Weapon
{
    public Havoc()
    {
        WeaponType = "Light Machine Gun(LMG)";
    }
}
public class Player
{
    public string PlayerName { get; set; }
    public Weapon Weapon { get; set; }
}
//Enemy
public abstract class Enemy
{
    public string EnemyType { get; set; }
}
public class Octane : Enemy
{
    public Octane()
    {
        EnemyType = "Speedster";
    }
}
public class Wraith : Enemy
{
    public Wraith()
    {
        EnemyType = "Interdimensional Jumper";
    }
}
public class Noob : Enemy
{
    public Noob()
    {
        EnemyType = "Doesn't know how to Play";
    }
}
//Upgrades
//Create multiple special abilities. Each one should only allow the player to "win" against one type of enemy.
public abstract class Updagrade : Weapon
{
    public Weapon Weapon { get; set;}
    public string WeaponType
    {
        get { return Weapon.WeaponType; }
        set { Weapon.WeaponType = value; }
    }
}
public class ExtendedMag : Updagrade
{
    public ExtendedMag(Weapon weapon)
    {
        Weapon = weapon;
    }
}
public class TurboCharger : Updagrade
{
    public TurboCharger(Weapon weapon)
    {
        Weapon = weapon;
    }
}