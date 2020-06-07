import React from "react";
import styled from "styled-components";

interface CharacterWindowProps {
    items: Set<string>,
    events: Set<string>,
    currentStory: number,
    allStoriesCount: number
}

const Window = styled.div`
    width: 40%;
    display: flex;
    flex-wrap: wrap;
    align-content: center;
`;

const UserElements = styled.div`
    display: flex;
    justify-content: space-evenly;
    width: 100%;
    padding: 10px;
`;

const Thing = styled.div`
    width: 50%;
    padding: 10px;
`;

const Break = styled.div`
  flex-basis: 100%;
  height: 0;
`;

const CharacterWindow = (props: CharacterWindowProps) => {
    // @ts-ignore
    const listItems = [...props.items].map( it => <div key={it}>{it}</div>);
    // @ts-ignore
    const listEvents = [...props.events].map( it => <div key={it}>{it}</div>);
    return (
        <Window>
            <UserElements>
                <Thing>Posiadane rzeczy: {listItems}</Thing>
                <Thing>Wydarzenia: {listEvents}</Thing>
            </UserElements>
            <Break />
        </Window>
    )
};


export default CharacterWindow;