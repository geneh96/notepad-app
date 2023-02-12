import React, { Component } from 'react';
import NewNote from './NewNote';

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { notes: [], loading: true };
    }

    componentDidMount() {
        this.populateNotepadData();
    }

    static renderNotepadTable(notes) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Date</th>
                        <th>Title</th>
                        <th>Notes</th>
                    </tr>
                </thead>
                <tbody>
                    {notes.map(notes =>
                        <tr key={notes.date}>
                            <td>{notes.date}</td>
                            <td>{notes.title}</td>
                            <td>{notes.summary}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Home.renderNotepadTable(this.state.notepadData);

        return (
            <div>
                <h1 id="tabelLabel" >Welcome to your notepad</h1>
                <p>This is a simple applications where you can view and update your notes.</p>
                <NewNote/>
                {contents}
            </div>
        );
    }

    async populateNotepadData() {
        const response = await fetch('notepad');
        const data = await response.json();
        this.setState({ notepadData: data, loading: false });
    }
}

