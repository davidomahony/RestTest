using System;

namespace WebApplication1
{
    public class PokemonNotFoundException : Exception
    {
        public PokemonNotFoundException(string name) : base($"This Pokemons records do not exist {name}")
        {
        }
    }
}
