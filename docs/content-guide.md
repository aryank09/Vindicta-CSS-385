# Content Guide for Vindicta Website

This guide will help you fill in all the placeholder content in your website.

## 1. Game Description Section

### Replace these placeholders in `index.html`:

**Genre & Gameplay:**
- `[INSERT GENRE]` - Examples: "Action-Platformer", "Puzzle Adventure", "2D Side-scroller"
- `[INSERT CORE GAMEPLAY CONCEPT]` - Brief description of main gameplay loop
- `[INSERT SETTING]` - Where/when your game takes place
- `[INSERT MAIN OBJECTIVE]` - What the player is trying to accomplish
- `[INSERT KEY MECHANICS]` - Main gameplay mechanics

**Influences & Inspiration:**
- `[INSERT INFLUENCES]` - Games, movies, books, or experiences that inspired you
- `[INSERT UNIQUE FEATURE]` - What makes your game different from others

**Features List:**
- `[INSERT UNIQUE FEATURE 1-4]` - 4 bullet points highlighting key features

**Game Specs:**
- `[INSERT UNITY VERSION]` - Your Unity version (e.g., "2022.3.21f1")
- `[INSERT PROJECTED RATING]` - ESRB rating (E, T, M)
- `[INSERT DATE]` - Launch/completion date
- `[INSERT TIMEFRAME]` - Development duration

## 2. Playable Game Embed

**For WebGL Build:**
1. Build your game for WebGL in Unity
2. Upload the build folder to your repository 
3. Replace the placeholder div with:
```html
<iframe src="./webgl-build/index.html" 
        width="960" 
        height="600" 
        frameborder="0" 
        allowfullscreen>
</iframe>
```

**For Download Links:**
- Upload Windows and Mac builds to your repository
- Replace `href="#"` with actual file paths
- Update `[INSERT SIZE]` with actual file sizes

## 3. Trailer Section

**YouTube Embed:**
1. Upload your trailer to YouTube
2. Get the video ID from the URL (everything after `v=`)
3. Replace `YOUR_VIDEO_ID` in the iframe src

**Trailer Description:**
- `[INSERT SPECIFIC SCENES]` - Describe what's shown in the trailer

## 4. Playtesting Reports

Fill in these sections for each of your 3 playtests:

**For Each Playtest:**
- Development phase when conducted
- Number and demographics of participants  
- Testing method used (in-person, remote, etc.)
- Key feedback received
- Changes made based on feedback

**For Final Playtest (minimum 10 players):**
- Detailed ratings/scores if collected
- Specific quotes from players
- Planned changes for future versions
- Recommendations for other developers

## 5. Team Credits

**For Each Team Member:**
- Full name
- Primary role(s)
- Email (optional)
- 2-3 key contributions

## 6. Source Code Section

- `[INSERT GITHUB/REPOSITORY URL]` - Link to your GitHub repo
- `[INSERT DOWNLOAD ZIP URL]` - Direct download link for source code
- `[INSERT WEBSITE URL]` - URL of this website
- `[INSERT DATE]` - Last update date

## 7. Asset References

Create a table with all external assets:

| Asset Name | Type | Source URL | License |
|------------|------|------------|---------|
| Background Music | Audio | freesound.org/... | CC0 |
| Player Sprite | Image | opengameart.org/... | CC BY 3.0 |

**Make sure all assets are:**
- Free for non-commercial use
- Properly attributed
- License clearly stated

## 8. Team Reflection

### Question 1: "What did your team do really well?"
- Choose ONE specific thing
- Provide concrete evidence
- Examples: communication, technical implementation, project management

### Question 2: "Top 3 priorities for 2 more weeks?"
1. **Priority 1:** Specific improvement with explanation
2. **Priority 2:** Specific addition with reasoning  
3. **Priority 3:** Specific fix/enhancement with justification

## 9. Presentations

**Upload Requirements:**
- Postmortem: 2 slides (5 things right, 5 things wrong)
- Final presentation: 5 slides from demo day
- Convert to PDF for web embedding
- Use Google Slides, PowerPoint Online, or PDF.js for embedding

## Additional Tips

### Visual Consistency
- Use consistent terminology throughout
- Keep tone professional but engaging
- Proofread everything before publishing

### Accessibility
- Add alt text for all images
- Use descriptive link text
- Ensure color contrast is sufficient
- Test with screen readers if possible

### Testing
- Test website on different devices
- Check all links work
- Verify all media loads properly
- Get feedback from classmates

### File Organization
```
your-repo/
├── index.html
├── styles.css
├── README.txt
├── screenshots/
│   ├── screenshot1.png
│   ├── screenshot2.png
│   └── ...
├── presentations/
│   ├── postmortem.pdf
│   └── final-presentation.pdf
├── builds/
│   ├── windows-build.zip
│   └── mac-build.zip
└── webgl-build/
    ├── index.html
    └── ...
``` 