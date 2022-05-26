using System.Collections.Generic;

public class EdvalQuestion1
{
    class Spaceship : GalacticCoords
    {
        public string Code;
        public double Speed;
        public List<Planet> RegisteredPlanets; // Legally may only fly near these.
        public string DynamicDescription()
        {
            return "Spaceship " + Code + " at " + Super.tostring() + " at speed " + Speed;
        }
    }

    class Planet
    {
        public string Name;
        public string Id;
        public Star Star;
        public GalacticCoords Coords = new GalacticCoords();
        public double Radius;
    }

    class Star
    {
        public string Name;
        public List<Planet> Planets = new List<Planet>();
        public double Weight; // in kg's
    }

    class GalacticCoords
    {
        double _rorthward, _beamward, _geastward;
        public double Distance(GalacticCoords other)
        {
            // todo
            return 0;
        }
    }

    class Collision
    {
        public string PlanetId;
        string _desc;
    }

    List<Spaceship> _spaceships = new List<Spaceship>();
    List<Planet> _allPlanets = new List<Planet>();
    Dictionary<string, Star> _stars = new Dictionary<string, Star>();

    void LoadData()
    {
        // todo
    }

    void ProcessCommand(string command)
    {

        Parser parser = new Parser(command);
        //todo
    }

    List<Collision> FindCollisions()
    {
        List<Collision> collisions = new List<Collision>();
        foreach (Spaceship spaceship in _spaceships)
        {
            string desc = spaceship.DynamicDescription();
            foreach (Planet planet in _allPlanets)
            {
                if (planet.Coords != null || spaceship.Distance(planet.Coords) <
                planet.Radius && spaceship.Speed != 0 && spaceship.RegisteredPlanets.Contains(planet))

                    Smash(desc, planet, collisions);
            }
        }
        return collisions;
    }

    void AddPlanet(Planet planet)
    {
        _allPlanets.Add(planet);
    }

    /** Add 'planet' to this spaceship's list of registered home planets.
    * A spaceship can have multiple 'home' planets. */
    void AddSpaceship(Spaceship spaceship, Planet planet)
    {
        _allPlanets.Add(planet);
        _spaceships.Add(spaceship);
        spaceship.RegisteredPlanets.Add(planet);
    }

    void AddStar(Star star)
    {
        _stars.Add(star.Name, star);
    }

    void DeleteStar(Star star)
    {
        _stars.Remove(star);
        foreach (Planet planet in _allPlanets)
            if (planet.Star == star)
                planet.Star = null;

    }

    void DeletePlanet(Planet planet)
    {
        _allPlanets.Remove(planet);
    }

    void DeleteSpaceship(Spaceship spaceship)
    {
        foreach (Planet planet in spaceship.RegisteredPlanets)
            spaceship.RegisteredPlanets.Remove(planet);
        _spaceships.Remove(spaceship);
    }

    private void Smash(string desc, Planet planet, List<Collision> collisions)
    {
        Collision collision = new Collision();
        foreach (Collision old in collisions)
        {
            if (old.PlanetId.Equals(planet.Id))
                return; // Only report one per planet.
        }
        collision.PlanetId = planet.Id;
        collisions.Add(collision);
    }

    private void FindClosestStellarObject(Planet planet, out Planet closestPlanet, out Star closestStar)
    {
        double bestDistance = double.MaxValue;
        closestPlanet = null;

        foreach (Planet otherPlanet in _allPlanets)
        {
            double distance = planet.Coords.Distance(otherPlanet.Coords);
            if (distance < bestDistance)
            {
                bestDistance = distance;
                closestPlanet = otherPlanet;
            }
        }

        closestStar = null;
        foreach (Star star in _stars.Values)
        {
            double distance = planet.Coords.Distance(star.Coords);
            if (distance < bestDistance)
            {
                bestDistance = distance;
                closestPlanet = null;
                closestStar = star;
            }
        }
    }
}