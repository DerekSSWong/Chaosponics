using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Elements;

public class Invoice : IEnumerable<Element>
{
	protected Dictionary<Element, float> data = new Dictionary<Element, float>();
	protected bool flagged;
	
	public Invoice() {
		flagged = false;
		foreach(Element e in Element.GetValues(typeof(Element))) {
			data.Add(e, 0f);
		}
	}
	
	public void setVal(Element e, float val) {
		data[e] = val;
	}
	
	public float getVal(Element e) {
		return data[e];
	}
	
	public void flag() {
		flagged = true;
	}
	
	public bool isFlagged() {
		return flagged;
	}
	
	public void add(Invoice inv) {
		foreach (Element e in inv) {
			data[e] += inv.getVal(e);
		}
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
