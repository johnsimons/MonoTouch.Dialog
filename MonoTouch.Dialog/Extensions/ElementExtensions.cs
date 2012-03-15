using System;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace MonoTouch.Dialog
{
	public static class ElementExtensions
	{
		private static readonly UIFont font = UIFont.BoldSystemFontOfSize(17);
		
		/// <summary>
		/// Computes the X position for the entry by aligning all the entries in the Section
		/// </summary>
		/// <returns>
		/// The entry position.
		/// </returns>
		/// <param name='elem'>
		/// Element.
		/// </param>
		/// <param name='tv'>
		/// Tv.
		/// </param>
		/// <param name='cell'>
		/// Cell.
		/// </param>
		public static SizeF ComputeEntryPosition(this Element elem, UITableView tv, UITableViewCell cell)
		{
			var s = elem.Parent as Section;
			if (s.EntryAlignment.Width != 0)
				return s.EntryAlignment;

			// If all EntryElements have a null Caption, align UITextField with the Caption
			// offset of normal cells (at 10px).
			var max = new SizeF(-15, tv.StringSize("M", font).Height);
			foreach (var ee in s.Elements)
			{
				if (ee.Caption != null)
				{
					var size = tv.StringSize(ee.Caption, font);
					if (size.Width > max.Width)
						max = size;
				}
			}
			s.EntryAlignment = new SizeF(25 + Math.Min(max.Width, 160), max.Height);
			
			return s.EntryAlignment;
		}
		
		public static void MoveToNextResponder(this Element elem){
				
			RootElement root = elem.GetImmediateRootElement();
      		IUIResponder focus = null;

      		if (root == null)
      			return;

      		foreach (var s in root.Sections)
      		{
      			foreach (var e in s.Elements)
      			{
      				if (e == elem)
      				{
      					focus = elem as IUIResponder;
      				}
      				else if (focus != null && e is IUIResponder)
      				{
      					focus = e as IUIResponder;
      					break;
      				}
      			}

      			if (focus != null && focus != elem)
      				break;
      		}

      		if (focus != elem)
      			focus.BecomeFirstResponder(true);
      		else
      			focus.ResignFirstResponder(true);
		}
		
		public static void MoveToPreviousResponder(this Element elem){
			RootElement root = elem.GetImmediateRootElement();
      		IUIResponder focus = null;
			IUIResponder prevElem = null;
			
      		if (root == null)
      			return;

      		foreach (var s in root.Sections)
      		{
      			foreach (var e in s.Elements)
      			{
      				if (e == elem)
      				{
      					focus = elem as IUIResponder;
      				}
					
      				if (focus != null && prevElem != null)
      				{
      					focus = prevElem;
      					break;
      				}
					
					if (e is IUIResponder)
      				{
						prevElem = (IUIResponder)e;
					}
      			}

      			if (focus != null && focus != elem)
      				break;
      		}

      		if (focus != elem)
      			focus.BecomeFirstResponder(true);
      		else
      			focus.ResignFirstResponder(true);
		}
	}
}

