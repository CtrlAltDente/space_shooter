using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loadbar : MonoBehaviour
{
    [SerializeField]
    private Image _loaderImage;

    [SerializeField]
    private Sprite[] _textures;

    private int _currentImageNumber;

    private void OnEnable()
    {
        StartCoroutine(ChangeImages(0.25f));
    }

    private IEnumerator ChangeImages(float delay)
    {
        _currentImageNumber = 0;
        _loaderImage.sprite = _textures[_currentImageNumber];

        while (gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(delay);

            _loaderImage.sprite = GetNextImage();

            yield return null;
        }
    }

    private Sprite GetNextImage()
    {
        _currentImageNumber++;

        if(_currentImageNumber > _textures.Length - 1)
        {
            _currentImageNumber = 0;
        }

        return _textures[_currentImageNumber];
    }
}
