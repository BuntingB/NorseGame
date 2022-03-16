/* Script for button functionality
 * Programmer: Brandon Bunting
 * Date Created: 03/15/2022
 * Date Modified: 03/15/2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonDefinition : MonoBehaviour
{
    //
    public bool _animated = false;
    public Color _unselectedTint = Color.grey;
    public Color _selectedTint = Color.white;
    public bool _selected = false;
    Button _button;
    Image _image;
    Animator _animator;

    public AudioClip _swapToSFX;
    public AudioClip _confirmSFX;
    public float _confirmTime;

    bool _disableControls = false;

    // Runs when button becomes active
    void OnEnable()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();

        _animated = TryGetComponent<Animator>(out _animator);

        if (!_animated)
        {
            _image.color = _selected ? _selectedTint : _unselectedTint;
        }
    }

    // Runs when button is swapped to
    public void SwappedTo()
    {
        _selected = true;

        if (_swapToSFX != null)
        {
            AudioSource.PlayClipAtPoint(_swapToSFX, Vector3.zero);
        }

        if (_animated)
        {
            _animator.SetBool("Selected", _selected);
        }
        else
        {
            _image.color = _selectedTint;
        }
    }

    // Runs when button is swapped off
    public void SwappedOff()
    {
        _selected = false;

        if (_animated)
        {
            _animator.SetBool("Selected", _selected);
        }
        else
        {
            _image.color = _unselectedTint;
        }
    }

    // Runs when button is activated
    public IEnumerator ClickButton()
    {
        if (!_disableControls)
        {
            _disableControls = true;

            if (_confirmSFX != null)
            {
                AudioSource.PlayClipAtPoint(_confirmSFX, Vector3.zero);
            }

            yield return new WaitForSeconds(_confirmTime);

            _disableControls = false;

            _button.onClick.Invoke();
        }
    }

    //
    public bool GetDisableControls()
    {
        return _disableControls;
    }

}
