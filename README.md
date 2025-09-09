# WPF-Partical-Animation
A stunning real-time particle animation system built with WPF and .NET 9, featuring dynamic particle swarms and realistic bubble physics with mathematical expressions.
# WPF Particle Swarm & Bubble Animation

A stunning real-time particle animation system built with WPF and .NET 9, featuring dynamic particle swarms and realistic bubble physics with mathematical expressions.

![Animation Demo](https://img.shields.io/badge/WPF-Particle%20Animation-blue) ![.NET 9](https://img.shields.io/badge/.NET-9.0-purple) ![License](https://img.shields.io/badge/License-MIT-green)

## 🎯 Features

### ✨ Particle Swarms
- **Dynamic Swarm Behavior**: Particles follow complex orbital patterns around moving centers
- **Mathematical Expressions**: Movement based on sine/cosine functions with phase variations
- **Organic Motion**: Multi-layered noise patterns for realistic particle behavior
- **Color Dynamics**: Real-time color changes based on velocity and movement intensity
- **Boundary Wrapping**: Seamless screen edge transitions

### 🫧 Bubble Masses
- **Realistic Physics**: Buoyancy simulation with upward movement and oscillation
- **Life Cycle System**: Bubbles fade, pop, and regenerate naturally
- **Size Pulsation**: Dynamic sizing with breathing effects
- **Gradient Rendering**: Beautiful radial gradients with transparency
- **Floating Dynamics**: Horizontal drift patterns with vertical oscillation

### 🎮 Interactive Controls
- **Add Particle Swarm**: Generate new swarms with random colors
- **Add Bubble Mass**: Create new bubble clusters
- **Clear All**: Remove all active animations
- **Pause/Resume**: Toggle animation playback

## 🚀 Quick Start

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Windows OS (WPF requirement)
- VS Code with C# Dev Kit extension (recommended)

### Installation

1. **Clone or Download**
   ```bash
   git clone <repository-url>
   cd WPFParticleAnimation
   ```

2. **Build and Run**
   ```bash
   dotnet build
   dotnet run
   ```

### Alternative: Manual Setup
```bash
# Create new project
mkdir WPFParticleAnimation && cd WPFParticleAnimation
dotnet new wpf -n ParticleAnimationWPF
cd ParticleAnimationWPF

# Replace default files with provided code
# Copy Program.cs content
# Update .csproj file

dotnet run
```

## 📁 Project Structure

```
WPFParticleAnimation/
└── ParticleAnimationWPF/
    ├── Program.cs                 # Main application code
    ├── ParticleAnimationWPF.csproj  # Project configuration
    └── bin/                       # Build outputs
```

## 🔧 Technical Details

### Architecture
- **Pure Code-Behind**: No XAML files - everything rendered programmatically
- **Object-Oriented Design**: Separate classes for Particles and Bubbles
- **60 FPS Animation**: Smooth rendering with DispatcherTimer
- **Mathematical Foundation**: Physics-based movement algorithms

### Key Algorithms

#### Particle Movement
```csharp
// Orbital motion with varying radius
var orbitX = CenterX + Math.Sin(time * 0.8 + Phase) * OrbitRadius * (0.5 + 0.5 * Math.Sin(time * 0.2));
var orbitY = CenterY + Math.Cos(time * 0.8 + Phase) * OrbitRadius * (0.5 + 0.5 * Math.Cos(time * 0.15));

// Multi-layered noise for organic movement
var noiseX = (Math.Sin(time * 3 + Phase) + Math.Sin(time * 1.7 + Phase * 2)) * 0.3;
var noiseY = (Math.Cos(time * 2.5 + Phase) + Math.Cos(time * 2.1 + Phase * 1.5)) * 0.3;
```

#### Bubble Physics
```csharp
// Buoyancy and oscillation
var buoyancy = -0.3; // Upward force
var horizontalDrift = Math.Sin(time * 1.5 + Phase) * 0.8;
var verticalOscillation = Math.Sin(time * 2 + Phase * 1.5) * 0.2;

// Size pulsation
var pulseFactor = 1 + Math.Sin(time * 3 + Phase) * 0.15;
```

## 🎨 Customization

### Adding New Particle Types
Extend the base `Particle` class:
```csharp
public class FireParticle : Particle
{
    // Custom fire particle behavior
}
```

### Modifying Animation Parameters
```csharp
// In Particle.Update() method
OrbitRadius = 50 + random.NextDouble() * 100;  // Orbit size
VelocityX *= 0.95;  // Damping factor
Phase = random.NextDouble() * Math.PI * 2;  // Phase variation
```

### Color Schemes
```csharp
var colors = new[]
{
    Colors.Cyan, Colors.Magenta, Colors.Yellow,
    Colors.Orange, Colors.LimeGreen, Colors.DeepPink
};
```

## 🛠️ Development Setup

### VS Code Setup
1. Install extensions:
   - C# Dev Kit
   - C# Language Support
   - .NET Install Tool

2. Create tasks.json for build automation:
```json
{
    "version": "2.0.0",
    "tasks": [
        {
            "label": "build",
            "command": "dotnet",
            "type": "process",
            "args": ["build"],
            "group": "build"
        }
    ]
}
```

### Visual Studio Setup
1. Open `.sln` file or folder
2. Set startup project
3. Build and run (F5)

## 📊 Performance

- **Target FPS**: 60 FPS
- **Particle Count**: ~40 particles per swarm + 15 bubbles per mass
- **Memory Usage**: Low overhead with efficient object reuse
- **CPU Usage**: Optimized with minimal mathematical operations

## 🐛 Troubleshooting

### Common Issues

**"SDK not found" Error**
```bash
dotnet --list-sdks
# Ensure .NET 9 is installed
```

**Build Errors**
```bash
dotnet clean
dotnet restore
dotnet build
```

**Nullable Reference Warnings**
- Either use the fixed code provided
- Or disable nullable in `.csproj`: `<Nullable>disable</Nullable>`

**Performance Issues**
- Reduce particle count in `AddParticleSwarm()` method
- Increase timer interval for lower FPS

## 🎯 Use Cases

- **Educational**: Physics simulation and mathematical visualization
- **Prototyping**: UI animation concepts and particle systems
- **Entertainment**: Screensavers and visual effects
- **Learning**: WPF animation techniques and .NET 9 features

## 🤝 Contributing

1. Fork the repository
2. Create feature branch: `git checkout -b feature/new-particle-type`
3. Commit changes: `git commit -m 'Add new particle type'`
4. Push to branch: `git push origin feature/new-particle-type`
5. Submit pull request

## 📚 Learning Resources

- [WPF Documentation](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/)
- [.NET 9 Features](https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9)
- [Mathematical Animation Techniques](https://www.mathworks.com/help/matlab/animation.html)

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- Inspired by natural particle systems and fluid dynamics
- Mathematical expressions based on physics simulation principles
- WPF animation techniques from Microsoft documentation

## 🔮 Future Enhancements

- [ ] 3D particle effects with perspective
- [ ] Collision detection between particles
- [ ] Custom particle shapes and textures
- [ ] Audio-reactive particle behavior
- [ ] Export animation as video/GIF
- [ ] GPU acceleration with DirectX

---

**Made with ❤️ using WPF and .NET 9**

*For questions or support, please open an issue on GitHub.*
