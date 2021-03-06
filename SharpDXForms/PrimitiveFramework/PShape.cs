﻿using System.Collections.Generic;
using Poly2Tri;
using SharpDX;

namespace SharpDXForms.PrimitiveFramework
{
	public class PShape : Primitive
	{
		private List<Vector2> vertices;

		public PShape( PShape shape )
			: base( shape )
		{
			this.vertices = shape.vertices;
		}

		public PShape( bool filled )
			: base( filled )
		{
			vertices = new List<Vector2>();
		}

		public Vector2 FirstPoint
		{
			get { return vertices[ 0 ]; }
		}

		public List<Vector2> Points
		{
			get { return vertices; }
		}

		public void AddVertices( List<Vector2> vertices )
		{
			foreach( Vector2 vertex in vertices )
			{
				AddVertex( vertex );
			}
		}

		public void AddVertex( float x, float y )
		{
			AddVertex( new Vector2( x, y ) );
		}

		public void AddVertex( Vector2 vertex )
		{
			vertices.Add( vertex );
			primitiveCreated = false;
		}

		public void InsertVertex( int index, float x, float y )
		{
			InsertVertex( index, new Vector2( x, y ) );
		}

		public void InsertVertex( int index, Vector2 vertex )
		{
			vertices.Insert( index, vertex );
			primitiveCreated = false;
		}

		internal override List<PolygonPoint> GetPoints()
		{
			List<PolygonPoint> points = new List<PolygonPoint>( vertices.Count );
			foreach( Vector2 vertex in vertices )
			{
				points.Add( new PolygonPoint( vertex.X, vertex.Y ) );
			}

			//if( !Filled && points.Count > 2 )
			//{
			//	points.Add( points[ 0 ] );
			//}

			//// Offset all points by distance to the centroid. This places all points around (0, 0).
			//Vector2 center = GetCentroid( points );
			//foreach( PolygonPoint point in points )
			//{
			//	point.X -= center.X;
			//	point.Y -= center.Y;
			//}

			return points;
		}
	}
}