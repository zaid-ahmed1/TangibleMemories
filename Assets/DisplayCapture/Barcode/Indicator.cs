using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Anaglyph.DisplayCapture.Barcodes
{
	public class Indicator : MonoBehaviour
	{
		[SerializeField] private LineRenderer lineRenderer;
		public LineRenderer LineRenderer => lineRenderer;

		private Postgres postgres;

		[SerializeField] private TMP_Text textMesh;
		public TMP_Text TextMesh => textMesh;

		private Vector3[] offsetPositions = new Vector3[4];


		void Awake()
		{
			postgres = FindFirstObjectByType<Postgres>();

			if (postgres == null)
			{
				Debug.LogError("Could not find a Postgres instance in the scene.");
			}
		}
		
		public void Set(BarcodeTracker.Result result) => Set(result.text, result.corners);

		public void Set(string text, Vector3[] corners)
		{
			if (text.StartsWith("https://tangible-moments.me/"))
			{
				// Remove the prefix
				text = text.Substring(28);
				Vector3 topCenter = (corners[2] + corners[3]) / 2f;
				transform.position = topCenter;

				Vector3 up = (corners[1] - corners[0]).normalized;
				Vector3 right = (corners[2] - corners[1]).normalized;
				Vector3 normal = -Vector3.Cross(up, right).normalized;

				Vector3 center = (corners[2] + corners[0]) / 2f;

				for (int i = 0; i < 4; i++)
				{
					Vector3 dir = (corners[i] - center).normalized;
					offsetPositions[i] = corners[i] + (dir * 0.1f);
				}

				transform.rotation = Quaternion.LookRotation(normal, up);

				if (lineRenderer != null)
				{
					lineRenderer.SetPositions(offsetPositions);
				}
				else
				{
					Debug.LogError("LineRenderer is null on Indicator.");
				}

				if (textMesh != null && postgres != null)
				{
					var memory = postgres.FindMemoryByQRCode(text);
					if (memory != null)
					{
						textMesh.text = memory.qr_code;
						PlayerPrefs.SetString("currentMemoryFileKey", memory.filekey);
						PlayerPrefs.Save();
					}
					else
					{
						Debug.LogWarning($"No memory found for QR code: {text}");
						textMesh.text = "(Unknown QR)";
					}
				}
				else
				{
					if (textMesh == null) Debug.LogError("TextMesh is null on Indicator.");
					if (postgres == null) Debug.LogError("Postgres component is null on Indicator.");
				}
			}
		}
	}
}