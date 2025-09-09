using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ParticleAnimationWPF
{
    public partial class MainWindow : Window
    {
        private Canvas animationCanvas;
        private List<Particle> particles;
        private List<Bubble> bubbles;
        private DispatcherTimer animationTimer;
        private Random random;
        private double time = 0;

        public MainWindow()
        {
            InitializeComponent();
            SetupAnimation();
        }

        private void InitializeComponent()
        {
            Title = "WPF Particle Swarm & Bubble Animation";
            Width = 1200;
            Height = 800;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Create main layout
            var mainPanel = new StackPanel();
            
            // Control panel
            var controlPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(10),
                Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0))
            };

            var particleButton = new Button { Content = "Add Particle Swarm", Margin = new Thickness(5) };
            var bubbleButton = new Button { Content = "Add Bubble Mass", Margin = new Thickness(5) };
            var clearButton = new Button { Content = "Clear All", Margin = new Thickness(5) };
            var pauseButton = new Button { Content = "Pause/Resume", Margin = new Thickness(5) };

            particleButton.Click += (s, e) => AddParticleSwarm();
            bubbleButton.Click += (s, e) => AddBubbleMass();
            clearButton.Click += (s, e) => ClearAll();
            pauseButton.Click += (s, e) => TogglePause();

            controlPanel.Children.Add(particleButton);
            controlPanel.Children.Add(bubbleButton);
            controlPanel.Children.Add(clearButton);
            controlPanel.Children.Add(pauseButton);

            // Animation canvas
            animationCanvas = new Canvas
            {
                Background = new LinearGradientBrush(
                    Color.FromRgb(10, 25, 40),
                    Color.FromRgb(5, 15, 25),
                    90)
            };

            mainPanel.Children.Add(controlPanel);
            mainPanel.Children.Add(animationCanvas);

            Content = mainPanel;
        }

        private void SetupAnimation()
        {
            random = new Random();
            particles = new List<Particle>();
            bubbles = new List<Bubble>();

            animationTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(16) // ~60 FPS
            };
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();

            // Add initial particle swarms and bubble masses
            AddParticleSwarm();
            AddBubbleMass();
        }

        private void AddParticleSwarm()
        {
            var centerX = random.NextDouble() * (animationCanvas.ActualWidth - 200) + 100;
            var centerY = random.NextDouble() * (animationCanvas.ActualHeight - 200) + 100;
            var swarmColor = GenerateRandomColor();

            for (int i = 0; i < 25; i++)
            {
                var particle = new Particle(
                    centerX + (random.NextDouble() - 0.5) * 100,
                    centerY + (random.NextDouble() - 0.5) * 100,
                    centerX,
                    centerY,
                    swarmColor,
                    random);

                particles.Add(particle);
                animationCanvas.Children.Add(particle.Shape);
            }
        }

        private void AddBubbleMass()
        {
            if (animationCanvas.ActualWidth == 0 || animationCanvas.ActualHeight == 0)
            {
                // Use default values if canvas not yet measured
                var width = Math.Max(animationCanvas.Width, 800);
                var height = Math.Max(animationCanvas.Height, 600);
                
                var centerX = random.NextDouble() * (width - 200) + 100;
                var centerY = random.NextDouble() * (height - 200) + 100;

                for (int i = 0; i < 15; i++)
                {
                    var bubble = new Bubble(
                        centerX + (random.NextDouble() - 0.5) * 80,
                        centerY + (random.NextDouble() - 0.5) * 80,
                        random);

                    bubbles.Add(bubble);
                    animationCanvas.Children.Add(bubble.Shape);
                }
            }
            else
            {
                var centerX = random.NextDouble() * (animationCanvas.ActualWidth - 200) + 100;
                var centerY = random.NextDouble() * (animationCanvas.ActualHeight - 200) + 100;

                for (int i = 0; i < 15; i++)
                {
                    var bubble = new Bubble(
                        centerX + (random.NextDouble() - 0.5) * 80,
                        centerY + (random.NextDouble() - 0.5) * 80,
                        random);

                    bubbles.Add(bubble);
                    animationCanvas.Children.Add(bubble.Shape);
                }
            }
        }

        private Color GenerateRandomColor()
        {
            var colors = new[]
            {
                Colors.Cyan, Colors.Magenta, Colors.Yellow, Colors.Orange,
                Colors.LimeGreen, Colors.DeepPink, Colors.Violet, Colors.Gold
            };
            return colors[random.Next(colors.Length)];
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            time += 0.016;

            // Update particles
            foreach (var particle in particles)
            {
                particle.Update(time, animationCanvas.ActualWidth, animationCanvas.ActualHeight);
            }

            // Update bubbles
            foreach (var bubble in bubbles)
            {
                bubble.Update(time, animationCanvas.ActualWidth, animationCanvas.ActualHeight);
            }
        }

        private void ClearAll()
        {
            foreach (var particle in particles)
                animationCanvas.Children.Remove(particle.Shape);
            foreach (var bubble in bubbles)
                animationCanvas.Children.Remove(bubble.Shape);

            particles.Clear();
            bubbles.Clear();
        }

        private void TogglePause()
        {
            if (animationTimer.IsEnabled)
                animationTimer.Stop();
            else
                animationTimer.Start();
        }
    }

    public class Particle
    {
        public Ellipse Shape { get; private set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double CenterX { get; set; }
        public double CenterY { get; set; }
        public double VelocityX { get; set; }
        public double VelocityY { get; set; }
        public double Phase { get; set; }
        public double OrbitRadius { get; set; }
        public Color BaseColor { get; set; }
        private Random random;

        public Particle(double x, double y, double centerX, double centerY, Color color, Random rnd)
        {
            X = x;
            Y = y;
            CenterX = centerX;
            CenterY = centerY;
            BaseColor = color;
            random = rnd;
            Phase = random.NextDouble() * Math.PI * 2;
            OrbitRadius = 50 + random.NextDouble() * 100;
            VelocityX = (random.NextDouble() - 0.5) * 2;
            VelocityY = (random.NextDouble() - 0.5) * 2;

            Shape = new Ellipse
            {
                Width = 4 + random.NextDouble() * 6,
                Height = 4 + random.NextDouble() * 6,
                Fill = new SolidColorBrush(color)
            };

            Canvas.SetLeft(Shape, X);
            Canvas.SetTop(Shape, Y);
        }

        public void Update(double time, double canvasWidth, double canvasHeight)
        {
            // Swarm behavior with mathematical expressions
            var swarmForceX = Math.Sin(time * 0.5 + Phase) * 0.5;
            var swarmForceY = Math.Cos(time * 0.3 + Phase) * 0.5;

            // Orbit around center with varying radius
            var orbitX = CenterX + Math.Sin(time * 0.8 + Phase) * OrbitRadius * (0.5 + 0.5 * Math.Sin(time * 0.2));
            var orbitY = CenterY + Math.Cos(time * 0.8 + Phase) * OrbitRadius * (0.5 + 0.5 * Math.Cos(time * 0.15));

            // Attraction to orbit position
            var attractionX = (orbitX - X) * 0.02;
            var attractionY = (orbitY - Y) * 0.02;

            // Noise for organic movement
            var noiseX = (Math.Sin(time * 3 + Phase) + Math.Sin(time * 1.7 + Phase * 2)) * 0.3;
            var noiseY = (Math.Cos(time * 2.5 + Phase) + Math.Cos(time * 2.1 + Phase * 1.5)) * 0.3;

            // Update velocity with combined forces
            VelocityX += swarmForceX + attractionX + noiseX;
            VelocityY += swarmForceY + attractionY + noiseY;

            // Damping
            VelocityX *= 0.95;
            VelocityY *= 0.95;

            // Update position
            X += VelocityX;
            Y += VelocityY;

            // Boundary wrapping
            if (X < 0) X = canvasWidth;
            if (X > canvasWidth) X = 0;
            if (Y < 0) Y = canvasHeight;
            if (Y > canvasHeight) Y = 0;

            // Update center position (slowly drifting)
            CenterX += Math.Sin(time * 0.1 + Phase) * 0.2;
            CenterY += Math.Cos(time * 0.08 + Phase) * 0.2;

            // Keep center in bounds
            CenterX = Math.Max(100, Math.Min(canvasWidth - 100, CenterX));
            CenterY = Math.Max(100, Math.Min(canvasHeight - 100, CenterY));

            // Dynamic color based on velocity and time
            var intensity = Math.Min(1.0, Math.Sqrt(VelocityX * VelocityX + VelocityY * VelocityY) / 3.0);
            var alpha = (byte)(100 + intensity * 155);
            var dynamicColor = Color.FromArgb(alpha, BaseColor.R, BaseColor.G, BaseColor.B);
            Shape.Fill = new SolidColorBrush(dynamicColor);

            // Update visual position
            Canvas.SetLeft(Shape, X - Shape.Width / 2);
            Canvas.SetTop(Shape, Y - Shape.Height / 2);
        }
    }

    public class Bubble
    {
        public Ellipse Shape { get; private set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double VelocityY { get; set; }
        public double Phase { get; set; }
        public double Size { get; set; }
        public double MaxSize { get; set; }
        public double Life { get; set; }
        public Color BubbleColor { get; set; }
        private Random random;

        public Bubble(double x, double y, Random rnd)
        {
            X = x;
            Y = y;
            random = rnd;
            Phase = random.NextDouble() * Math.PI * 2;
            Size = 5 + random.NextDouble() * 15;
            MaxSize = Size + random.NextDouble() * 20;
            Life = random.NextDouble();
            VelocityY = -(0.5 + random.NextDouble() * 1.5); // Upward movement
            BubbleColor = Color.FromArgb(80, 100, 200, 255); // Semi-transparent blue

            Shape = new Ellipse
            {
                Width = Size,
                Height = Size,
                Fill = new RadialGradientBrush(
                    Color.FromArgb(120, 150, 220, 255),
                    Color.FromArgb(40, 100, 180, 200))
                {
                    Center = new Point(0.3, 0.3),
                    GradientOrigin = new Point(0.3, 0.3)
                },
                Stroke = new SolidColorBrush(Color.FromArgb(60, 255, 255, 255)),
                StrokeThickness = 1
            };

            Canvas.SetLeft(Shape, X);
            Canvas.SetTop(Shape, Y);
        }

        public void Update(double time, double canvasWidth, double canvasHeight)
        {
            // Bubble physics with expressions
            var buoyancy = -0.3; // Upward force
            var horizontalDrift = Math.Sin(time * 1.5 + Phase) * 0.8;
            var verticalOscillation = Math.Sin(time * 2 + Phase * 1.5) * 0.2;

            // Update velocity
            VelocityY += buoyancy + verticalOscillation;

            // Update position
            X += horizontalDrift;
            Y += VelocityY;

            // Size pulsation
            var pulseFactor = 1 + Math.Sin(time * 3 + Phase) * 0.15;
            var currentSize = Size * pulseFactor * (0.5 + Life * 0.5);
            
            Shape.Width = currentSize;
            Shape.Height = currentSize;

            // Life cycle - bubbles pop and regenerate
            Life -= 0.005;
            if (Life <= 0 || Y < -50)
            {
                // Regenerate bubble at bottom
                Y = canvasHeight + 50;
                X = random.NextDouble() * canvasWidth;
                Life = 1.0;
                Size = 5 + random.NextDouble() * 15;
                Phase = random.NextDouble() * Math.PI * 2;
                VelocityY = -(0.5 + random.NextDouble() * 1.5);
            }

            // Boundary constraints for X
            if (X < -20) X = canvasWidth + 20;
            if (X > canvasWidth + 20) X = -20;

            // Dynamic transparency based on life and position
            var alpha = (byte)(40 + Life * 80);
            var dynamicColor = Color.FromArgb(alpha, BubbleColor.R, BubbleColor.G, BubbleColor.B);
            
            var gradient = new RadialGradientBrush(
                Color.FromArgb((byte)(alpha * 1.5), 150, 220, 255),
                Color.FromArgb((byte)(alpha * 0.7), 100, 180, 200))
            {
                Center = new Point(0.3, 0.3),
                GradientOrigin = new Point(0.3, 0.3)
            };
            
            Shape.Fill = gradient;

            // Update visual position
            Canvas.SetLeft(Shape, X - Shape.Width / 2);
            Canvas.SetTop(Shape, Y - Shape.Height / 2);
        }
    }
}

// Program.cs
namespace ParticleAnimationWPF
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new MainWindow());
        }
    }
}