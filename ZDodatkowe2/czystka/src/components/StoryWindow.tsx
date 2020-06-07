import React, {useState} from "react";
import styled from "styled-components";
import TextArea from "./TextArea";
import ButtonToolbar from "./ButtonToolbar";
import stories from "../story.json";
import CharacterWindow from "./CharacterWindow";
import ProgressBar from "./ProgressBar";

const Window = styled.div`
    display: flex;
    flex-direction: column;
    align-items: center
`;

const HorizontalLine = styled.div`
    margin-top: auto;
    height: 10px;
    width: 100%;
    background: #000;
`;


const StoryWindow = () => {
    const [ pickedStories, setPickedStories ] = useState<number[]>([0]);
    const [ addedThings, setAddedThings ] = useState<Set<string>>(new Set());
    const [ eventsSurvived, setEventsSurvived ] = useState<Set<string>>(new Set());
    const [ chances, setChances ] = useState<number>(0);
    const [ percentage, setPercentage] = useState<number>(0);

    const storiesObj = stories.stories;
    const currentStory = storiesObj[pickedStories[pickedStories.length - 1]];

    const updateStoryId = (id: number, recentChances: number) => {
        const checkIfDecision = checkDecision(id, pickedStories, chances + recentChances, addedThings);
        const pickedStoryId = checkIfDecision === 0 ? id : checkIfDecision;
        const tmpStories = pickedStories.concat(pickedStoryId!! - 1);
        setPickedStories(tmpStories);
        if (checkIfDecision) updateStoryId(pickedStoryId!!, 0);
    };

    const updateUserElements = (things: Set<string>, events: Set<string>) => {
        setAddedThings(things);
        setEventsSurvived(events);
    };

    const addChances = (addValue: number) => {
        if (addValue === 0) setChances(0);
        else setChances(chances + addValue);
    };

    if (percentage < 100 * (pickedStories[pickedStories.length - 1] / (storiesObj.length - 1)))
        setPercentage(100 * (pickedStories[pickedStories.length - 1]/ (storiesObj.length - 1)));


    return (
        <Window>
            <ProgressBar percentage={percentage}/>
            <TextArea text={currentStory.text}/>
            <ButtonToolbar buttons={currentStory.buttons}
                           updateId={updateStoryId}
                           addChances={addChances}
                           items={addedThings}
                           events={eventsSurvived}
                           updateUserElements={updateUserElements}
            />
            <HorizontalLine/>
            <CharacterWindow items={addedThings}
                             events={eventsSurvived}
                             currentStory={pickedStories[pickedStories.length - 1]}
                             allStoriesCount={storiesObj.length}/>

        </Window>
    )
};

const checkDecision = (id: number, pickedStories: number[], chances: number, items: Set<string>) => {
    const storiesObj = stories.stories;
    const currentStory = storiesObj[id - 1];
    if (currentStory.text === "CHECK") {
        return (currentStory.routes!!.find(it => pickedStories.includes(it.picked - 1))!!.goto);
    } else if (currentStory.text === "CHANCE" && currentStory.percent !== undefined) {
        const percent = currentStory.percent === "var" ? chances : currentStory.percent;
        const random = Math.random() * 100;
        return random <= percent ? currentStory.success : currentStory.failure;
    } else if (currentStory.text === "CHECKITEM") {
        if (currentStory.item && items.has(currentStory.item)) return currentStory.goto;
        return currentStory.else;
    } else {
        return 0;
    }
};
export default StoryWindow;