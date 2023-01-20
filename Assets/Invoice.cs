using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Elements;

public class Invoice : IEnumerable<Element>
{	
	/// <summary>
	/// Used to pass Element data back and forth Nutrient classes. Also used to store Element requirements for plants.
	/// </summary>
	protected Dictionary<Element, float> data = new Dictionary<Element, float>();
	protected bool flagged;
	bool empty;
	
	/// <summary>
	/// Creates an empty invoice
	/// </summary>
	/// <returns></returns>
	public Invoice() {
		flagged = false;
		empty = true;
		foreach(Element e in Element.GetValues(typeof(Element))) {
			data.Add(e, 0f);
		}
	}
	
	/// <summary>
	/// Duplicates an invoice from an existing invoice
	/// </summary>
	/// <param name="invoice"></param>
	/// <returns></returns>
	public Invoice (Invoice invoice) {
		flagged = false;
		empty = invoice.isEmpty();
		foreach (Element e in invoice) {
			data.Add(e, invoice.getVal(e));
		}
	}
	
	public void setVal(Element e, float val) {
		empty = false;
		data[e] = val;
	}
	
	public float getVal(Element e) {
		return data[e];
	}
	
	
	/// <summary>
	/// If unable to completely withdraw from nutrient, invoice is flagged
	/// </summary>
	public void flag() {
		flagged = true;
	}
	
	public bool isFlagged() {
		return flagged;
	}
	
	public void add(Invoice inv) {
		empty = false;
		foreach (Element e in inv) {
			data[e] += inv.getVal(e);
		}
	}
	
	public void minus(Invoice inv) {
		empty = true;
		foreach (Element e in inv) {
			data[e] -= inv.getVal(e);
			if (data[e] <= 0) {
				data[e] = 0;
			}
			else {
				empty = false;
			}
		}
	}
	
	public void mult(float buff) {
		foreach (Element val in Element.GetValues(typeof(Element))) {
			data[val] *= buff;
		}
	}
	
	public bool isEmpty() {
		return empty;
	}
	
	public IEnumerator<Element> GetEnumerator() {
		foreach(Element e in Element.GetValues(typeof(Element))) {
			yield return e;
		}
	}
	
	IEnumerator IEnumerable.GetEnumerator() {
		return GetEnumerator();
	}
}
