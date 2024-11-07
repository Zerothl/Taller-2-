using System;
using System.Collections.Generic;

public interface ICombate
{
    int Atacar();
    int Defender();
}

public interface IEquipo
{
    void AgregarJugador(Jugador jugador);
    int CalcularRendimiento();
}

public abstract class Pokemon : ICombate
{
    private string nombre;
    private string tipo;
    private int[] ataques;
    private int defensa;

    public Pokemon(string nombre, string tipo, int ataque1, int ataque2, int ataque3, int defensa)
    {
        this.nombre = nombre;
        this.tipo = tipo;
        this.ataques = new int[] { ataque1, ataque2, ataque3 };
        this.defensa = defensa;
    }

    public string Nombre { get { return nombre; } }
    public string Tipo { get { return tipo; } }
    public int Defensa { get { return defensa; } }
    public int[] Ataques { get { return ataques; } }

    public abstract int Atacar();
    public abstract int Defender();
}

public class Pikachu : Pokemon
{
    public Pikachu(string nombre, string tipo, int ataque1, int ataque2, int ataque3, int defensa) 
        : base(nombre, tipo, ataque1, ataque2, ataque3, defensa) { }

    public override int Atacar()
    {
        Random rand = new Random();
        int ataqueSeleccionado = Ataques[rand.Next(Ataques.Length)];
        return ataqueSeleccionado * rand.Next(0, 2);  
    }

    public override int Defender()
    {
        Random rand = new Random();
        return (int)(Defensa * (rand.Next(0, 2) == 0 ? 1 : 0.5)); 
    }
}

public class Charizard : Pokemon
{
    public Charizard(string nombre, string tipo, int ataque1, int ataque2, int ataque3, int defensa)
        : base(nombre, tipo, ataque1, ataque2, ataque3, defensa) { }

    public override int Atacar()
    {
        Random rand = new Random();
        int ataqueSeleccionado = Ataques[rand.Next(Ataques.Length)];
        return ataqueSeleccionado * rand.Next(0, 2); 
    }

    public override int Defender()
    {
        Random rand = new Random();
        return (int)(Defensa * (rand.Next(0, 2) == 0 ? 1 : 0.5)); 
    }
}

public class Jugador
{
    private string nombre;
    private string posicion;
    private int rendimiento;

    public Jugador(string nombre, string posicion, int rendimiento)
    {
        this.nombre = nombre;
        this.posicion = posicion;
        this.rendimiento = rendimiento;
    }

    public string Nombre { get { return nombre; } }
    public string Posicion { get { return posicion; } }
    public int Rendimiento { get { return rendimiento; } }
}

public class Equipo : IEquipo
{
    private List<Jugador> jugadores;

    public Equipo()
    {
        jugadores = new List<Jugador>();
    }

    public void AgregarJugador(Jugador jugador)
    {
        jugadores.Add(jugador);
    }

    public int CalcularRendimiento()
    {
        int totalRendimiento = 0;
        foreach (var jugador in jugadores)
        {
            totalRendimiento += jugador.Rendimiento;
        }
        return totalRendimiento;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Seleccione el ejercicio a ejecutar:");
        Console.WriteLine("1. Batalla de Pokémon");
        Console.WriteLine("2. Partido de Basket");
        int opcion;
        
        if (int.TryParse(Console.ReadLine(), out opcion))
        {
            if (opcion == 1)
            {
                EjercicioPokemon();
            }
            else if (opcion == 2)
            {
                EjercicioBasket();
            }
            else
            {
                Console.WriteLine("Opción no válida.");
            }
        }
        else
        {
            Console.WriteLine("Entrada no válida. Debes ingresar un número.");
        }
    }

    public static void EjercicioPokemon()
    {
        Console.WriteLine("Ingrese los datos del Pokemon 1 (nombre, tipo, 3 ataques, defensa): ");
        string nombre1 = Console.ReadLine() ?? "Desconocido";  
        string tipo1 = Console.ReadLine() ?? "Desconocido";    
        int ataque1_1 = PedirNumero("Ataque 1: ");
        int ataque1_2 = PedirNumero("Ataque 2: ");
        int ataque1_3 = PedirNumero("Ataque 3: ");
        int defensa1 = PedirNumero("Defensa: ");

        Console.WriteLine("Ingrese los datos del Pokemon 2 (nombre, tipo, 3 ataques, defensa): ");
        string nombre2 = Console.ReadLine() ?? "Desconocido";
        string tipo2 = Console.ReadLine() ?? "Desconocido";
        int ataque2_1 = PedirNumero("Ataque 1: ");
        int ataque2_2 = PedirNumero("Ataque 2: ");
        int ataque2_3 = PedirNumero("Ataque 3: ");
        int defensa2 = PedirNumero("Defensa: ");

        Pokemon pokemon1 = new Pikachu(nombre1, tipo1, ataque1_1, ataque1_2, ataque1_3, defensa1);
        Pokemon pokemon2 = new Charizard(nombre2, tipo2, ataque2_1, ataque2_2, ataque2_3, defensa2);

        int puntosPokemon1 = 0;
        int puntosPokemon2 = 0;

        for (int i = 0; i < 3; i++)
        {
            puntosPokemon1 += pokemon1.Atacar();
            puntosPokemon1 += pokemon1.Defender();

            puntosPokemon2 += pokemon2.Atacar();
            puntosPokemon2 += pokemon2.Defender();
        }

        Console.WriteLine("Puntos de " + pokemon1.Nombre + ": " + puntosPokemon1);
        Console.WriteLine("Puntos de " + pokemon2.Nombre + ": " + puntosPokemon2);

        if (puntosPokemon1 > puntosPokemon2)
            Console.WriteLine("Ganó " + pokemon1.Nombre);
        else if (puntosPokemon1 < puntosPokemon2)
            Console.WriteLine("Ganó " + pokemon2.Nombre);
        else
            Console.WriteLine("Empate");
    }

    public static void EjercicioBasket()
    {
        List<Jugador> jugadoresRegistrados = new List<Jugador>
        {
            new Jugador("Juan", "Alero", 8),
            new Jugador("Carlos", "Base", 7),
            new Jugador("Pedro", "Pivot", 9),
            new Jugador("Luis", "Alero", 6),
            new Jugador("Sergio", "Base", 10),
            new Jugador("Miguel", "Pivot", 5)
        };

        Random rand = new Random();
        Equipo equipo1 = new Equipo();
        Equipo equipo2 = new Equipo();

        for (int i = 0; i < 3; i++)
        {
            int index = rand.Next(jugadoresRegistrados.Count);
            equipo1.AgregarJugador(jugadoresRegistrados[index]);
            jugadoresRegistrados.RemoveAt(index);

            index = rand.Next(jugadoresRegistrados.Count);
            equipo2.AgregarJugador(jugadoresRegistrados[index]);
            jugadoresRegistrados.RemoveAt(index);
        }

        int rendimientoEquipo1 = equipo1.CalcularRendimiento();
        int rendimientoEquipo2 = equipo2.CalcularRendimiento();

        Console.WriteLine("Rendimiento del Equipo 1: " + rendimientoEquipo1);
        Console.WriteLine("Rendimiento del Equipo 2: " + rendimientoEquipo2);

        if (rendimientoEquipo1 > rendimientoEquipo2)
            Console.WriteLine("Ganó el Equipo 1");
        else if (rendimientoEquipo1 < rendimientoEquipo2)
            Console.WriteLine("Ganó el Equipo 2");
        else
            Console.WriteLine("Empate");
    }

    public static int PedirNumero(string mensaje)
    {
        int numero;
        Console.WriteLine(mensaje);
        while (!int.TryParse(Console.ReadLine(), out numero))
        {
            Console.WriteLine("Entrada no válida. Por favor ingrese un número entero.");
        }
        return numero;
    }
}
