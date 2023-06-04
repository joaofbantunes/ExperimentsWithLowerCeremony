# Experimenting with lower ceremony

The goal of this repo, was just for me to experiment a bit with lower ceremony approaches to projects following DDD ideas.

The motivation was mostly around the fact that, while DDD ideas make a lot of sense when building applications with a focus on the business, it feels like we tend to focus too much on tactical patterns, some arbitrary definition of "clean code", ending up not only with what looks like over-engineering, as well as leaving some performance on the table.

As I've been working in an event-driven microservices context, and feeling like there's something we can do to improve and simplify things, I've started thinking what could we do, and if there are characteristics of this context that could help with that.

I ended up thinking about multiple things, including how we could minimize some infrastructure waste (e.g. always moving an aggregate back and forth), and ended up considering if events could help with that. At some point, I came across Oskar's [How events can help in making the state-based approach efficient](https://event-driven.io/en/how_events_can_help_on_making_state_based_approach_efficient/) post, which validated some of the options I was considering, so you'll notice inspiration from that post in this code.

> I thought about this approach with events event without event sourcing, when I was thinking about ORMs and their change tracker, and if we got rid of the ORM, how could we do things (without implementing our own change tracker). Turns out, even without event sourcing, events can be our change tracker, and we can make use of them to implement efficient persistence logic.

At the end of the day, the idea is to try to remove at least a bit of the ceremony typically associated when using domain focused approaches to code, be a bit more performance conscious, while still trying to keep things decoupled (i.e. domain logic from infrastructure).

Loose things I was thinking with these experiments:

- While still using common tactical patterns, reduce a bit the ceremony, in particular around having related code spread around multiple projects
- Reduce some indirections when they don't add value, include them if they do (i.e. separate command handler from the API endpoint code?)
- Make use of some approaches typically more associated with functional programming, like making better use of the type system, relying less on exceptions and using explicit domain errors, pure functions, etc
- Try to minimize, even if slightly wasteful infrastructure usage
- Further get rid of tactical patters, while still maintaining domain and infrastructure separation, in order to create more streamlined and performance optimized solutions

Following these ideas, created some example API projects to test things out:

- ABitLessCeremony - uses some common DDD tactical patterns, but in a (hopefully) simpler and lighter way than the usual
- YetAnotherBitLessCeremony - still uses some common DDD tactical patterns, but drops some indirections, e.g. command handler only classes
- YetMoreOptimizationOfCeremony - while still trying to decouple domain logic from infrastructure, lets go of the typical DDD tactical patterns, in order to further streamline things, minimizing potentially wasteful infrastructure usage (e.g. loading an aggregate root completely, when just partially would suffice)

> Note: the APIs in this repository don't run. I just wanted to experiment with some ideas on how to broadly organize things, and couldn't be bothered to implement everything to make it run 😅

Keep in mind these are just some experiments, and incomplete for that matter. I used this as an exercise for myself, just thought of sharing in case it can help someone out that's going through the same questions as me, like "can't we build things nicely but in a simpler way?".

For example the project structure wasn't something I gave tremendous thought. My ideas in this regard were mostly:

- Don't create multiple projects, this doesn't make the difference that folks think it does
    - If one wants to enforce some structure, one can use something like [ArchUnitNET](https://github.com/TNG/ArchUnitNET)
- Keep related things relatively close, instead of spread into too many folders

Other than that, things got into the place they are a bit organically, but I'm pretty sure that when I try this in an actual project, or at least give things more thought, things are probably gonna be moved around a bit.
