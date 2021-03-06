///////////////////////////////////////////////////////////
//  ThreePointArcNode.cs
//  Implementation of the Class ThreePointArcNode
//  Generated by Enterprise Architect
//  Created on:      08-四月-2011 13:45:33
//  Original author: Administrator
///////////////////////////////////////////////////////////




using DIST.DGP.DataExchange.VCT.FileData;
namespace DIST.DGP.DataExchange.VCT.FileData {
	/// <summary>
	/// VCT三点圆弧节点类
	/// </summary>
	public class ThreePointArcNode : SegmentNode {

		private PointInfoNode PointNode1;
		private PointInfoNode PointNode2;
		private PointInfoNode PointNode3;
		public PointInfoNode m_PointInfoNode;
		/// <summary>
		/// 第一点
		/// </summary>
		private PointInfoNode m_PointInfoNode1;
		/// <summary>
		/// 第二点
		/// </summary>
		private PointInfoNode m_PointInfoNode2;
		/// <summary>
		/// 第三点
		/// </summary>
		private PointInfoNode m_PointInfoNode3;

        public ThreePointArcNode()
            : base(12)
        {

		}

        //~ThreePointArcNode(){

        //}

        //public override void Dispose(){

        //}

	}//end ThreePointArcNode

}//end namespace FileData