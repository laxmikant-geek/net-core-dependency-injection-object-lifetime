# .NET Dependency Injection Object Lifetimes

Companion sample for the GeeksArray tutorial
[.NET Dependency Injection Object Lifetimes](https://geeksarray.com/blog/net-core-dependency-injection-object-lifetime).

## What it demonstrates

Three identical services — `TransientCounter`, `ScopedCounter`, `SingletonCounter` —
each assigned a GUID at construction. One endpoint injects **two of each** so the
GUIDs prove how many instances actually exist:

| Lifetime | Within one request | Across requests |
|---|---|---|
| Transient | two different GUIDs | always new |
| Scoped | same GUID twice | new per request |
| Singleton | same GUID twice | same forever |

## Run it

```bash
dotnet run
curl http://localhost:<port>/ | python3 -m json.tool
curl http://localhost:<port>/ | python3 -m json.tool   # call again: singleton GUID unchanged, scoped changed
```

The article walks through the output line by line, then covers the captive-dependency
trap (never inject scoped into singleton), factory registrations, `IEnumerable<T>`
multi-registration, and scope validation options.
