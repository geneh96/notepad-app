import {React,  useRef} from 'react'

const NewNote = () => {
  const titleRef = useRef(undefined);
  const notesRef = useRef(undefined);

  const onCreateNewNote=(e)=>{
    e.preventDefault();

    const title = titleRef.current.value;
    const notes = notesRef.current.value;
    let notepad = {
      
        'Date': '',
        'Title': title,
        'Summary': notes
      
    }
    console.log(notepad)
    fetch('/notepad',{
      method: 'POST',
      headers:{
        'Content-type':'application/json'
      },
        body: JSON.stringify(notepad)
    });

    e.target.reset();

  }

  return (
    <div name='new-note'>
      <form onSubmit={onCreateNewNote}>
        <div className='form-group'>
          <input className='my-2' type="text" placeholder='Title' ref={titleRef}></input>
        </div>
        <div class="form-group">
          <textarea className="my-2 form-control" rows="2" maxLength={300} placeholder='New Notes' ref={notesRef}></textarea>
        </div>
        <button type="submit" className="my-2 btn btn-primary">Add New Note</button>
      </form>
    </div>
  )
}

export default NewNote