using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Client.Game;
namespace Client.Models
{
	public class Ball
	{

	
		public Color pieceColor;
		public Color Color { get; set; }
		public int Radius { get; }
		public Point Position { get; set; }
		public Point Velocity { get; set; } = new Point(4, 4);
	
		public void Move(Point startPosition)
		{
			
			int startH = 137;
			Position = new Point(startPosition.X, startH);
			
			int target = startPosition.Y;
			int height = 592;
			int num= height / 6;
			int plus = height / 6;
			if (num <= target)
			{
				Position = new Point(startPosition.X, startH+ num);
			
			}
			num += plus;

			
		}

		public void Draw(Graphics g, SolidBrush myBrush, Point Position, int Radius)
		{
			
			g.FillEllipse(myBrush, new Rectangle(Position, new Size(Radius * 2, Radius * 2)));
			myBrush.Dispose();
		
		}

		public void Freeze()
		{
			// Freeze ball position
			Velocity = new Point(0, 0);
		}
		public void Delete()
		{
			// Freeze ball position
			Velocity = new Point(0, 0);
		}

		public void drawGamePieceMoves(MouseEventArgs e, Graphics f)
		{

			


		}

	}
}
