﻿using System;
using System.Drawing;
using MonoTouch.Foundation;
using SQLite;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;


namespace TouchTracker
{
	[Table ("Lines")]
	public class Line // : NSObject // Archive method of saving
	{
		[PrimaryKey, AutoIncrement, MaxLength(8)]
		public int ID {get; set;}

		public float beginx {get; set;}
		public float beginy {get; set;}
		public float endx {get; set;}
		public float endy {get; set;}

		UIColor _color;

		[Ignore]
		public UIColor color {
			get {
				return _color;
			}
		}

		public void setColor()
		{
			double xDiff = this.end.X - this.begin.X;
			double yDiff = this.end.Y - this.begin.Y;
			double angle = Math.Atan2(yDiff, xDiff) * (180.0d / Math.PI);
			Console.WriteLine("Angle = {0}", angle);
			double red = Math.Abs(angle)/180.0d;
			double green = 1.0d - Math.Abs(angle)/180.0d;
			double blue = 0.0d;
			if (Math.Abs(angle) > 90.0d)
				blue = (Math.Abs(angle)-180.0d)/-90.0d;
			else
				blue = Math.Abs(angle)/90.0d;
			_color = new UIColor((float)red, (float)green, (float)blue, 1.0f);
		}

		public void setColor(UIColor clr)
		{
			_color = clr;
		}

		[Ignore]
		public PointF begin {
			get {
				return new PointF(beginx, beginy);
			}
			set {
				beginx = value.X;
				beginy = value.Y;
			}
		}

		[Ignore]
		public PointF end {
			get {
				return new PointF(endx, endy);
			}
			set {
				endx = value.X;
				endy = value.Y;
			}
		} 

		public Line()
		{
		}

		public void draw(CGContext context)
		{
			context.MoveTo(this.begin.X, this.begin.Y);
			context.AddLineToPoint(this.end.X, this.end.Y);

			_color.SetStroke();

			context.StrokePath();
		}

		// Archive method of saving
//		[Export("initWithCoder:")]
//		public Line(NSCoder decoder)
//		{
//			this.begin.X = decoder.DecodeFloat("beginx");
//			this.begin.Y = decoder.DecodeFloat("beginy");
//			this.end.X = decoder.DecodeFloat("endx");
//			this.end.Y =  decoder.DecodeFloat("endy");
//		}
//
//		public override void EncodeTo (NSCoder coder)
//		{
//			coder.Encode(this.begin.X, "beginx");
//			coder.Encode(this.begin.Y, "beginy");
//			coder.Encode(this.end.X, "endx");
//			coder.Encode(this.end.Y, "endy");
//		}
	}
}
