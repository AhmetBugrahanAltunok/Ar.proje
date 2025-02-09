using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GezegenSpawner : MonoBehaviour
{
    public GameObject[] gezegenPrefabs;
    public int gezegenSayisi = 10;
    public float minSpawnDistance = 5f;
    public float maxSpawnDistance = 15f;
    public float minScale = 0.5f, maxScale = 1.5f;
    public Transform cameraTransform;
    public Text YasamGezegenSayisiText;
    public Text YasamBelirtisiGezegenSayisiText;

    private List<GameObject> spawnedGezegenler = new List<GameObject>();
    private int yasamGezegenSayisi = 0;
    private int yasamBelirtisiGezegenSayisi = 0;

    private bool isDragging = false;
    private Vector2 lastTouchPosition;
    public float rotationSpeed = 10f;

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        SpawnGezegenler();
        UpdateGezegenSayisiText();
    }

    void Update()
    {
        HandleRotation();
    }

    void SpawnGezegenler()
    {
        Debug.Log("🌍 Gezegen spawn işlemi başladı...");

        if (gezegenPrefabs.Length == 0)
        {
            Debug.LogWarning("⚠ Gezegen prefabs listesi boş!");
            return;
        }

        List<GameObject> randomGezegenler = new List<GameObject>(gezegenPrefabs);
        ShuffleList(randomGezegenler);

        int gezegenSayisiGercek = Mathf.Min(gezegenSayisi, randomGezegenler.Count);

        for (int i = 0; i < gezegenSayisiGercek; i++)
        {
            GameObject gezegenPrefab = randomGezegenler[i];

            Vector3 spawnPosition = GetRandomSpawnPosition();
            float randomScale = Random.Range(minScale, maxScale);

            GameObject yeniGezegen = Instantiate(gezegenPrefab, spawnPosition, Quaternion.identity, transform);
            yeniGezegen.transform.localScale = Vector3.one * randomScale;

            bool yasamIhtimaliVar = Random.value < 0.5f;

            if (yasamIhtimaliVar)
            {
                yeniGezegen.tag = "YasamVar";
                CreateIndicator(yeniGezegen, Color.green, 1.5f);
                yasamGezegenSayisi++;

                bool yasamBelirtisiVar = Random.value < 0.5f;
                if (yasamBelirtisiVar)
                {
                    yeniGezegen.tag = "YasamBelirtisiVar";
                    CreateIndicator(yeniGezegen, new Color(1.0f, 0.55f, 0.0f), -1.5f); // Turuncu, gezegenin altında
                    yasamBelirtisiGezegenSayisi++;
                }
            }
            else
            {
                yeniGezegen.tag = "YasamYok";
            }

            spawnedGezegenler.Add(yeniGezegen);
            Debug.Log($"🫠 Gezegen oluşturuldu: {yeniGezegen.name}, Yaşam: {yasamIhtimaliVar}, Konum: {spawnPosition}, Boyut: {randomScale}");
        }

        Debug.Log($"✅ Toplam {spawnedGezegenler.Count} gezegen oluşturuldu.");
        UpdateGezegenSayisiText();
    }

    void CreateIndicator(GameObject gezegen, Color color, float yOffset)
    {
        GameObject indicator = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        indicator.transform.SetParent(gezegen.transform);
        indicator.transform.localPosition = new Vector3(0, yOffset, 0);
        indicator.transform.localScale = Vector3.one * 0.2f;

        Renderer indicatorRenderer = indicator.GetComponent<Renderer>();
        indicatorRenderer.material.color = color;

        Destroy(indicator.GetComponent<Collider>());
    }

    void UpdateGezegenSayisiText()
    {
        if (YasamGezegenSayisiText != null)
        {
            YasamGezegenSayisiText.text = "Yaşam Olan Gezegen Sayısı: " + yasamGezegenSayisi;
        }
        if (YasamBelirtisiGezegenSayisiText != null)
        {
            YasamBelirtisiGezegenSayisiText.text = "Yaşam Belirtisi Olan Gezegen Sayısı: " + yasamBelirtisiGezegenSayisi;
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 spawnPosition;
        bool uygunKonumBulundu = false;
        int maxDeneme = 50;
        int denemeSayisi = 0;

        do
        {
            float spawnDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
            spawnPosition = cameraTransform.position + (cameraTransform.forward * spawnDistance);
            spawnPosition += cameraTransform.right * Random.Range(-3f, 3f);
            spawnPosition += cameraTransform.up * Random.Range(-2f, 2f);

            uygunKonumBulundu = true;
            foreach (var gezegen in spawnedGezegenler)
            {
                if (Vector3.Distance(gezegen.transform.position, spawnPosition) < 3f)
                {
                    uygunKonumBulundu = false;
                    break;
                }
            }

            denemeSayisi++;
        } while (!uygunKonumBulundu && denemeSayisi < maxDeneme);

        return spawnPosition;
    }

    private Vector2 rotationVelocity;
    private float smoothTime = 0.1f;
    private float dampingFactor = 0.95f;

    void HandleRotation()
    {
        Vector2 delta = Vector2.zero;

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            if (!isDragging)
            {
                isDragging = true;
                lastTouchPosition = touchPosition;
            }
            else
            {
                delta = touchPosition - lastTouchPosition;
                lastTouchPosition = touchPosition;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Input.mousePosition;

            if (!isDragging)
            {
                isDragging = true;
                lastTouchPosition = mousePosition;
            }
            else
            {
                delta = (Vector2)mousePosition - lastTouchPosition;
                lastTouchPosition = mousePosition;
            }
        }
        else
        {
            isDragging = false;
        }

        if (delta != Vector2.zero)
        {
            rotationVelocity = Vector2.Lerp(rotationVelocity, delta, smoothTime);
        }

        if (rotationVelocity != Vector2.zero)
        {
            float rotationX = rotationVelocity.y * rotationSpeed * Time.deltaTime;
            float rotationY = -rotationVelocity.x * rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up, rotationY, Space.World);
            transform.Rotate(Vector3.right, rotationX, Space.World);

            rotationVelocity *= dampingFactor;

            if (rotationVelocity.magnitude < 0.01f)
            {
                rotationVelocity = Vector2.zero;
            }
        }
    }

    void ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    // Gezegen bilgilerini almak için getter metodları ekliyoruz
    public int GetToplamGezegenSayisi()
    {
        return gezegenSayisi;
    }

    public int GetYasamGezegenSayisi()
    {
        return yasamGezegenSayisi;
    }

    public int GetYasamBelirtisiGezegenSayisi()
    {
        return yasamBelirtisiGezegenSayisi;
    }


}