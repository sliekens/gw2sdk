# Basic usage

The entry point for API access is `GuildWars2.Gw2Client`. From there you can use IntelliSense to discover resources.

The `Gw2Client` has a single dependency on `System.Net.Http.HttpClient` which you must provide from your application code.

A very simple console app might look like this.

@[code cs{13,16,29,41}](../../samples/BasicUsage/Program.cs)

Output

> Daily achievements of Thursday, 22 December 2022  
>
> Daily Recommended Fractal-Scale 18  
> Complete  scale 18 fractal in the Fractals of the Mists through the portal in Lion's Arch.  
> Rewards Fractal Initiate's Research Chest (1)  
> Double-click to open.  
> Contains a Pristine Fractal Relic, a +1 Agony Infusion, a Large Mist Defensive Potion, 2 Fractal Encryptions, and a Fractal Research Page.  
> Bonus rewards with the Agony Channeler or the Recursive Resourcing Mastery.  
>
> Daily Recommended Fractal-Scale 27  
> Complete the fractal dungeon at this difficulty in the Fractals of the Mists through the portal in Lion's Arch.  
> Requires the mastery Follows Advice.  
> Rewards Fractal Adept's Research Chest (1)  
> Double-click to open.  
> Contains a Pristine Fractal Relic, 2 +1 Agony Infusions, a Large Mist Mobility Potion, 3 Fractal Encryptions, and a Fractal Research Page.  
> Bonus rewards with the Agony Channeler or Recursive Resourcing Mastery.  
>
> Daily Recommended Fractal-Scale 64  
> Complete the fractal dungeon at this difficulty in the Fractals of the Mists through the portal in Lion's Arch.  
> Requires the mastery Follows Advice.  
> Rewards Fractal Expert's Research Chest (1)  
> Double-click to open.  
> Contains a Pristine Fractal Relic, 2 +1 Agony Infusions, a Large Mist Offensive Potion, 3 Fractal Encryptions, and a Fractal Research Page.  
> Bonus rewards with the Agony Channeler or Recursive Resourcing Mastery.  
>
> ...

This code is not optimized. Every daily achievement results in 2 additional network requests being made to fetch the achievement details and item reward. In a real application, you might fetch all related data in as few network calls as possible.
