using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Elements;

//Container for nutrient in both soil and plants
public class Nutrient : IEnumerable<Element>
{	
	protected Dictionary<Element, float[]> nutrient = new Dictionary<Element, float[]>();
	
	public Nutrient() {	
		foreach(Element e in Element.GetValues(typeof(Element))) {
			nutrient.Add(e, new float[] {0f,0f});
		}
		
	}
	
	public Nutrient(Invoice invoice) {
		foreach (Element e in invoice) {
			float val = invoice.getVal(e);
			nutrient.Add(e, new float[] {val, val});
		}
	}
	
	public void setNutrient(Element e, float[] n) {
		nutrient[e] = n;
	}
	
	public float getVal(Element e) {
		return nutrient[e][0];
	}
	
	public void setVal(Element e, float val) {
		nutrient[e][0] = val;
	}
	
	public float getCap(Element e) {
		return nutrient[e][1];
	}
	
	public void setCap(Element e, float val) {
		nutrient[e][1] = val;
	}
	
	public void deposit(Element e, float val) {
		nutrient[e][0] += val;
		if (getVal(e) > getCap(e)) {
			Debug.Log(e + " Capacity reached");
			setVal(e, getCap(e));
		}
	}
	
	public void withdraw(Element e, float val) {
		nutrient[e][0] -= val;
		if (getVal(e) < 0) {
			Debug.Log(e + " Overdraft");
			setVal(e, 0f);
		}
	}
	
	public Invoice withdraw(Invoice inv) {
		Invoice result = new Invoice();
		foreach (Element e in inv) {
			float supply = getVal(e);
			float demand = inv.getVal(e);
			if (supply < demand) {result.flag();}
			float finalVal = Mathf.Min(supply, demand);
			result.setVal(e, finalVal);
			withdraw(e, finalVal);
		}
		
		return result;
	}
	
	public void deposit(Invoice inv) {
		foreach (Element e in inv) {
			deposit(e, inv.getVal(e));
		}
	}
	
	public void combine(Nutrient n) {
		foreach (Element e in n) {
			nutrient[e][0] += n.getVal(e);
			nutrient[e][1] += n.getVal(e);
		}
	}
	
	public float getPortion(Element e) {
		float total = 0f;
		foreach (var item in nutrient) {
			total += item.Value[0];
		}
		float portion = nutrient[e][0] / total;
		return portion;
	}
	
	public float getSaltWeight() {
		float salt = Mathf.Max(nutrient[Salt][0], 1f);
		float brimstone = Mathf.Max(nutrient[Brimstone][0], 1f);
		float weighted = salt * (salt / (salt + brimstone));
		return weighted;
	}
	
	public float getBrimstoneWeight() {
		float brimstone = Mathf.Max(nutrient[Brimstone][0], 1f);
		float salt = Mathf.Max(nutrient[Salt][0], 1f);
		float weighted = brimstone * (brimstone / (salt + brimstone));
		return weighted;
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
